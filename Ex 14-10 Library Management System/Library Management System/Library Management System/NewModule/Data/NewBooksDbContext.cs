using Library_Management_System.Entities;
using Library_Management_System.NewModule.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Library_Management_System.NewModule.Data
{
    public class NewBooksDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewBooksDbContext(DbContextOptions<NewBooksDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<CategoryMaster> Categories { get; set; }
        public DbSet<PublisherMaster> Publishers { get; set; }
        public DbSet<BookMaster> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }

        // ========================================================
        // AUTOMATED AUDITING & SOFT DELETE LOGIC
        // ========================================================
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "System";
            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = username;
                    entry.Entity.UpdatedBy = username;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdateCount = 0;
                    entry.Entity.IsActive = true;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.CreatedAt).IsModified = false;

                    entry.Entity.UpdatedBy = username;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdateCount++;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    // Intercept hard deletes and convert to soft delete
                    entry.State = EntityState.Modified;

                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.CreatedAt).IsModified = false;

                    entry.Entity.UpdatedBy = username;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdateCount++;
                    entry.Entity.IsActive = false;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. categories-master Table Mapping
            modelBuilder.Entity<CategoryMaster>().ToTable("categories-master");
            modelBuilder.Entity<CategoryMaster>().HasKey(c => c.CId);
            modelBuilder.Entity<CategoryMaster>().Property(c => c.CId)
                .HasColumnName("c_id")
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<CategoryMaster>().Property(c => c.CName).HasColumnName("c_name").IsRequired();

            // 2. publishers-master Table Mapping
            modelBuilder.Entity<PublisherMaster>().ToTable("publishers-master");
            modelBuilder.Entity<PublisherMaster>().HasKey(p => p.PId);
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PId)
                .HasColumnName("p_id")
                .UseIdentityColumn(1, 1); // AUTO-INCREMENT 1, 2, 3...
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PName).HasColumnName("p_name").IsRequired();
            // 3. books-master Table Mapping
            modelBuilder.Entity<BookMaster>().ToTable("books-master");
            modelBuilder.Entity<BookMaster>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookMaster>().Property(b => b.BookId)
                .HasColumnName("book_id")
                .UseIdentityColumn(1, 1); // AUTO-INCREMENT 1, 2, 3...
            modelBuilder.Entity<BookMaster>().Property(b => b.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<BookMaster>().Property(b => b.CId).HasColumnName("c_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.PId).HasColumnName("p_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.Isbn).HasColumnName("isbn");
            modelBuilder.Entity<BookMaster>().HasIndex(b => b.Isbn).IsUnique().HasFilter("[isbn] IS NOT NULL");
            // 4. Bookscopies Table Mapping
            modelBuilder.Entity<BookCopy>().ToTable("Bookscopies");
            modelBuilder.Entity<BookCopy>().HasKey(bc => bc.BookCopyId);
            modelBuilder.Entity<BookCopy>().Property(bc => bc.BookCopyId)
                .HasColumnName("book_copy_id")
                .UseIdentityColumn(1001, 1);
            modelBuilder.Entity<BookCopy>().Property(bc => bc.BookId).HasColumnName("book_id");
            modelBuilder.Entity<BookCopy>().Property(bc => bc.Status).HasColumnName("status").IsRequired();

            // 5. Foreign Key Relationships
            modelBuilder.Entity<BookMaster>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookMaster>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookCopy>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.Copies)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================================================
            // GLOBAL AUDITING COLUMN MAPPING
            // ========================================================
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property("CreatedBy").HasColumnName("created_by");
                    modelBuilder.Entity(entityType.ClrType).Property("UpdatedBy").HasColumnName("updated_by");
                    modelBuilder.Entity(entityType.ClrType).Property("CreatedAt").HasColumnName("created_time");
                    modelBuilder.Entity(entityType.ClrType).Property("UpdatedAt").HasColumnName("updated_at");
                    modelBuilder.Entity(entityType.ClrType).Property("UpdateCount").HasColumnName("update_count");
                    modelBuilder.Entity(entityType.ClrType).Property("IsActive").HasColumnName("is_active");

                    // Apply Global Query Filter for IsActive = true
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                    var propertyMethodInfo = typeof(Microsoft.EntityFrameworkCore.EF).GetMethod("Property")!.MakeGenericMethod(typeof(bool));
                    var isDeletedProperty = System.Linq.Expressions.Expression.Call(propertyMethodInfo, parameter, System.Linq.Expressions.Expression.Constant("IsActive"));

                    var compareExpression = System.Linq.Expressions.Expression.MakeBinary(
                        System.Linq.Expressions.ExpressionType.Equal,
                        isDeletedProperty,
                        System.Linq.Expressions.Expression.Constant(true));

                    var lambda = System.Linq.Expressions.Expression.Lambda(compareExpression, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    }
}