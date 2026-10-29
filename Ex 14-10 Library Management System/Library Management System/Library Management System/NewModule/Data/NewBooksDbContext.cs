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
        public DbSet<DepartmentMaster> Departments { get; set; }
        public DbSet<StudentMaster> Students { get; set; }
        public DbSet<IssueMaster> IssueMasters { get; set; }
        public DbSet<IssueDetailMaster> IssueDetails { get; set; }

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

            // ========================================================
            // SEQUENCES
            // ========================================================
            modelBuilder.HasSequence<int>("DepartmentSeq").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("StudentSeq").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("IssueSeq").StartsAt(1).IncrementsBy(1);

            // ========================================================
            // ISSUE MANAGEMENT MAPPINGS
            // ========================================================

            // 1. IssueMaster Mapping
            modelBuilder.Entity<IssueMaster>().ToTable("issue-master");
            modelBuilder.Entity<IssueMaster>().HasKey(i => i.IssueId);

            modelBuilder.Entity<IssueMaster>().Property(i => i.IssueId)
                .HasColumnName("issue_id")
                .HasDefaultValueSql("CONCAT('i', NEXT VALUE FOR IssueSeq)")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<IssueMaster>().Property(i => i.SId).HasColumnName("s_id").IsRequired();
            modelBuilder.Entity<IssueMaster>().Property(i => i.IssueDate).HasColumnName("issue_date").IsRequired();
            modelBuilder.Entity<IssueMaster>().Property(i => i.Status).HasColumnName("status").IsRequired();

            // Foreign Key from Issue to Student
            modelBuilder.Entity<IssueMaster>()
                .HasOne<StudentMaster>()
                .WithMany()
                .HasForeignKey(i => i.SId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. IssueDetailMaster Mapping
            modelBuilder.Entity<IssueDetailMaster>().ToTable("issue-detail-master");
            modelBuilder.Entity<IssueDetailMaster>().HasKey(id => id.IssueDetailId);

            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.IssueDetailId)
                .HasColumnName("issue_detail_id")
                .UseIdentityColumn(3001, 1); // Auto-increments starting at 3001

            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.IssueId).HasColumnName("issue_id").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.BookCopyId).HasColumnName("book_copy_id").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.DueDate).HasColumnName("due_date").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.Status).HasColumnName("status").IsRequired();

            // Foreign Key from IssueDetail to IssueMaster
            modelBuilder.Entity<IssueDetailMaster>()
                .HasOne(id => id.Issue)
                .WithMany()
                .HasForeignKey(id => id.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign Key from IssueDetail to BookCopy
            modelBuilder.Entity<IssueDetailMaster>()
                .HasOne(id => id.BookCopy)
                .WithMany()
                .HasForeignKey(id => id.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict);


            // ========================================================
            // STUDENT MANAGEMENT MAPPINGS
            // ========================================================

            // 1. DepartmentMaster
            modelBuilder.Entity<DepartmentMaster>().ToTable("department-master");
            modelBuilder.Entity<DepartmentMaster>().HasKey(d => d.DId);

            modelBuilder.Entity<DepartmentMaster>().Property(d => d.DId)
                .HasColumnName("d_id")
                .HasDefaultValueSql("CONCAT('D', NEXT VALUE FOR DepartmentSeq)") // Generates D1, D2, D3
                .ValueGeneratedOnAdd(); // Tells EF Core to fetch the ID from SQL after saving

            modelBuilder.Entity<DepartmentMaster>().Property(d => d.DName)
                .HasColumnName("d_name").IsRequired();

            // 2. StudentMaster
            modelBuilder.Entity<StudentMaster>().ToTable("students-master");
            modelBuilder.Entity<StudentMaster>().HasKey(s => s.SId);

            modelBuilder.Entity<StudentMaster>().Property(s => s.SId)
                .HasColumnName("s_id")
                .HasDefaultValueSql("CONCAT('S', NEXT VALUE FOR StudentSeq)") // Generates S1, S2, S3
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StudentMaster>().Property(s => s.SName)
                .HasColumnName("s_name").IsRequired();

            modelBuilder.Entity<StudentMaster>().Property(s => s.DId)
                .HasColumnName("d_id").IsRequired();

            // 3. Foreign Key Relationship
            modelBuilder.Entity<StudentMaster>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DId)
                .OnDelete(DeleteBehavior.Restrict);

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