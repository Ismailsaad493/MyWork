using Book_Library_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
    }
}