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
        public DbSet<ReturnMaster> ReturnMasters { get; set; }
        public DbSet<ReturnDetailMaster> ReturnDetails { get; set; }

        // ADDED: New Fine Tables
        public DbSet<FineMaster> FineMasters { get; set; }
        public DbSet<FineDetailMaster> FineDetails { get; set; }

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
            modelBuilder.HasSequence<int>("ReturnSeq").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ReturnDetailSeq").StartsAt(1).IncrementsBy(1);

            // ADDED: Fine Sequences
            modelBuilder.HasSequence<int>("FineSeq").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("FineDetailSeq").StartsAt(1).IncrementsBy(1);

            // ========================================================
            // FINE MANAGEMENT MAPPINGS
            // ========================================================

            // 1. FineMaster Mapping
            modelBuilder.Entity<FineMaster>().ToTable("fine-master");
            modelBuilder.Entity<FineMaster>().HasKey(f => f.FineId);

            modelBuilder.Entity<FineMaster>().Property(f => f.FineId)
                .HasColumnName("fine_id")
                .HasDefaultValueSql("CONCAT('F', NEXT VALUE FOR FineSeq)")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FineMaster>().Property(f => f.IssueDetailId).HasColumnName("issue_detail_id").IsRequired();
            modelBuilder.Entity<FineMaster>().Property(f => f.FineDate).HasColumnName("fine_date").IsRequired();
            modelBuilder.Entity<FineMaster>().Property(f => f.Status).HasColumnName("status").IsRequired();

            modelBuilder.Entity<FineMaster>().Property(f => f.FineAmount)
                .HasColumnName("fine_amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<FineMaster>()
                .HasOne(f => f.IssueDetail)
                .WithMany()
                .HasForeignKey(f => f.IssueDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. FineDetailMaster Mapping
            modelBuilder.Entity<FineDetailMaster>().ToTable("fine-detail-master");
            modelBuilder.Entity<FineDetailMaster>().HasKey(fd => fd.FineDetailId);

            modelBuilder.Entity<FineDetailMaster>().Property(fd => fd.FineDetailId)
                .HasColumnName("fine_detail_id")
                .HasDefaultValueSql("CONCAT('FD', NEXT VALUE FOR FineDetailSeq)")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<FineDetailMaster>().Property(fd => fd.FineId).HasColumnName("fine_id").IsRequired();
            modelBuilder.Entity<FineDetailMaster>().Property(fd => fd.ChargeType).HasColumnName("charge_type").IsRequired();

            modelBuilder.Entity<FineDetailMaster>().Property(fd => fd.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<FineDetailMaster>()
                .HasOne(fd => fd.Fine)
                .WithMany()
                .HasForeignKey(fd => fd.FineId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========================================================
            // RETURN MANAGEMENT MAPPINGS
            // ========================================================
            modelBuilder.Entity<ReturnMaster>().ToTable("return-master");
            modelBuilder.Entity<ReturnMaster>().HasKey(r => r.RId);
            modelBuilder.Entity<ReturnMaster>().Property(r => r.RId)
                .HasColumnName("r_id")
                .HasDefaultValueSql("CONCAT('R', NEXT VALUE FOR ReturnSeq)")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ReturnMaster>().Property(r => r.SId).HasColumnName("s_id").IsRequired();
            modelBuilder.Entity<ReturnMaster>().Property(r => r.ReturnDate).HasColumnName("return_date").IsRequired();
            modelBuilder.Entity<ReturnMaster>()
                .HasOne<StudentMaster>()
                .WithMany()
                .HasForeignKey(r => r.SId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReturnDetailMaster>().ToTable("return-detail-master");
            modelBuilder.Entity<ReturnDetailMaster>().HasKey(rd => rd.ReturnDetailId);
            modelBuilder.Entity<ReturnDetailMaster>().Property(rd => rd.ReturnDetailId)
                .HasColumnName("return_detail_id")
                .HasDefaultValueSql("CONCAT('RD', NEXT VALUE FOR ReturnDetailSeq)")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<ReturnDetailMaster>().Property(rd => rd.RId).HasColumnName("r_id").IsRequired();
            modelBuilder.Entity<ReturnDetailMaster>().Property(rd => rd.IssueDetailId).HasColumnName("issue_detail_id").IsRequired();
            modelBuilder.Entity<ReturnDetailMaster>().Property(rd => rd.Condition).HasColumnName("condition").IsRequired();
            modelBuilder.Entity<ReturnDetailMaster>()
                .HasOne(rd => rd.Return)
                .WithMany()
                .HasForeignKey(rd => rd.RId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ReturnDetailMaster>()
                .HasOne(rd => rd.IssueDetail)
                .WithMany()
                .HasForeignKey(rd => rd.IssueDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================================
            // ISSUE MANAGEMENT MAPPINGS
            // ========================================================
            modelBuilder.Entity<IssueMaster>().ToTable("issue-master");
            modelBuilder.Entity<IssueMaster>().HasKey(i => i.IssueId);
            modelBuilder.Entity<IssueMaster>().Property(i => i.IssueId)
                .HasColumnName("issue_id")
                .HasDefaultValueSql("CONCAT('i', NEXT VALUE FOR IssueSeq)")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<IssueMaster>().Property(i => i.SId).HasColumnName("s_id").IsRequired();
            modelBuilder.Entity<IssueMaster>().Property(i => i.IssueDate).HasColumnName("issue_date").IsRequired();
            modelBuilder.Entity<IssueMaster>().Property(i => i.Status).HasColumnName("status").IsRequired();
            modelBuilder.Entity<IssueMaster>()
                .HasOne<StudentMaster>()
                .WithMany()
                .HasForeignKey(i => i.SId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueDetailMaster>().ToTable("issue-detail-master");
            modelBuilder.Entity<IssueDetailMaster>().HasKey(id => id.IssueDetailId);
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.IssueDetailId)
                .HasColumnName("issue_detail_id")
                .UseIdentityColumn(3001, 1);
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.IssueId).HasColumnName("issue_id").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.BookCopyId).HasColumnName("book_copy_id").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.DueDate).HasColumnName("due_date").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>().Property(id => id.Status).HasColumnName("status").IsRequired();
            modelBuilder.Entity<IssueDetailMaster>()
                .HasOne(id => id.Issue)
                .WithMany()
                .HasForeignKey(id => id.IssueId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<IssueDetailMaster>()
                .HasOne(id => id.BookCopy)
                .WithMany()
                .HasForeignKey(id => id.BookCopyId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================================
            // STUDENT MANAGEMENT MAPPINGS
            // ========================================================
            modelBuilder.Entity<DepartmentMaster>().ToTable("department-master");
            modelBuilder.Entity<DepartmentMaster>().HasKey(d => d.DId);
            modelBuilder.Entity<DepartmentMaster>().Property(d => d.DId)
                .HasColumnName("d_id")
                .HasDefaultValueSql("CONCAT('D', NEXT VALUE FOR DepartmentSeq)")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<DepartmentMaster>().Property(d => d.DName).HasColumnName("d_name").IsRequired();

            modelBuilder.Entity<StudentMaster>().ToTable("students-master");
            modelBuilder.Entity<StudentMaster>().HasKey(s => s.SId);
            modelBuilder.Entity<StudentMaster>().Property(s => s.SId)
                .HasColumnName("s_id")
                .HasDefaultValueSql("CONCAT('S', NEXT VALUE FOR StudentSeq)")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<StudentMaster>().Property(s => s.SName).HasColumnName("s_name").IsRequired();
            modelBuilder.Entity<StudentMaster>().Property(s => s.DId).HasColumnName("d_id").IsRequired();
            modelBuilder.Entity<StudentMaster>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========================================================
            // BOOK MANAGEMENT MAPPINGS
            // ========================================================
            modelBuilder.Entity<CategoryMaster>().ToTable("categories-master");
            modelBuilder.Entity<CategoryMaster>().HasKey(c => c.CId);
            modelBuilder.Entity<CategoryMaster>().Property(c => c.CId).HasColumnName("c_id").UseIdentityColumn(1, 1);
            modelBuilder.Entity<CategoryMaster>().Property(c => c.CName).HasColumnName("c_name").IsRequired();

            modelBuilder.Entity<PublisherMaster>().ToTable("publishers-master");
            modelBuilder.Entity<PublisherMaster>().HasKey(p => p.PId);
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PId).HasColumnName("p_id").UseIdentityColumn(1, 1);
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PName).HasColumnName("p_name").IsRequired();

            modelBuilder.Entity<BookMaster>().ToTable("books-master");
            modelBuilder.Entity<BookMaster>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookMaster>().Property(b => b.BookId).HasColumnName("book_id").UseIdentityColumn(1, 1);
            modelBuilder.Entity<BookMaster>().Property(b => b.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<BookMaster>().Property(b => b.CId).HasColumnName("c_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.PId).HasColumnName("p_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.Isbn).HasColumnName("isbn");
            modelBuilder.Entity<BookMaster>().HasIndex(b => b.Isbn).IsUnique().HasFilter("[isbn] IS NOT NULL");

            modelBuilder.Entity<BookCopy>().ToTable("Bookscopies");
            modelBuilder.Entity<BookCopy>().HasKey(bc => bc.BookCopyId);
            modelBuilder.Entity<BookCopy>().Property(bc => bc.BookCopyId).HasColumnName("book_copy_id").UseIdentityColumn(1001, 1);
            modelBuilder.Entity<BookCopy>().Property(bc => bc.BookId).HasColumnName("book_id");
            modelBuilder.Entity<BookCopy>().Property(bc => bc.Status).HasColumnName("status").IsRequired();

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