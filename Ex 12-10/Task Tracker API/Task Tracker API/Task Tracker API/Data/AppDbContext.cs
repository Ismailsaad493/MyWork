using Microsoft.EntityFrameworkCore;
using Task_Tracker_API.Entities;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks { get; set; }


    }   

