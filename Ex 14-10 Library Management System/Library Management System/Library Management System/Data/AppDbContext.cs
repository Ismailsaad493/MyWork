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

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Fine> Fines { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "System";
            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = username;
                    entry.Entity.UpdatedBy = username;
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.UpdateCount = 0; // Starts at 0, increases on edits
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Lock creation metadata so it never changes on update
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.CreatedAt).IsModified = false;

                    entry.Entity.UpdatedBy = username;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdateCount++; // Automatically increments on each modification (1, 2, 3... 10)
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Correctly map BookLoan relationships using existing properties to avoid shadow keys
            modelBuilder.Entity<BookLoan>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookLoan>()
                .HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sets decimal precision for Fines
            modelBuilder.Entity<Fine>()
                .Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}