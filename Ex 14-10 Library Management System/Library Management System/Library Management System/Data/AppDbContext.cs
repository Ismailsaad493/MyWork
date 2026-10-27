using Library_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // All DbSets grouped together cleanly at the top
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Fine> Fines { get; set; }

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
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.UpdateCount = 0;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.CreatedAt).IsModified = false;

                    entry.Entity.UpdatedBy = username;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdateCount++;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        // Only ONE OnModelCreating method is allowed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---> ADDED: Tell SQL Server to auto-generate the UserCode using the Id
            modelBuilder.Entity<User>()
                .Property(u => u.UserCode)
                .HasComputedColumnSql("'LIB' + RIGHT('000' + CAST([Id] AS VARCHAR(10)), 3)", stored: true);

            // Correctly links BookLoan exclusively to Student
            modelBuilder.Entity<BookLoan>()
                .HasOne(l => l.Student)
                .WithMany(s => s.BookLoans)
                .HasForeignKey(l => l.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fine>()
                .Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}