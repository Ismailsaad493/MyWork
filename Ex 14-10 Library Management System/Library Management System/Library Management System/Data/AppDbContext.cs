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
            // Extract the Username directly from the token (Make sure this entire line is copied!)
            var username = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value ?? "Unknown User";

            var entries = ChangeTracker.Entries<IAuditable>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = username;
                    entry.Entity.UpdatedBy = username;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Ensure the original creator's name is never overwritten
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Entity.UpdatedBy = username;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}