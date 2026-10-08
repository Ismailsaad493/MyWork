using Microsoft.EntityFrameworkCore;
using Medicine_Management_API.Entities;

namespace Medicine_Management_API.Data
{
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options) { }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
