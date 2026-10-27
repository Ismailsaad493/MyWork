using Library_Management_System.NewModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.NewModule.Data
{
    public class NewBooksDbContext : DbContext
    {
        public NewBooksDbContext(DbContextOptions<NewBooksDbContext> options) : base(options) { }

        public DbSet<CategoryMaster> Categories { get; set; }
        public DbSet<PublisherMaster> Publishers { get; set; }
        public DbSet<BookMaster> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. categories-master Table Mapping
            modelBuilder.Entity<CategoryMaster>().ToTable("categories-master");
            modelBuilder.Entity<CategoryMaster>().HasKey(c => c.CId);
            modelBuilder.Entity<CategoryMaster>().Property(c => c.CId).HasColumnName("c_id");
            modelBuilder.Entity<CategoryMaster>().Property(c => c.CName).HasColumnName("c_name").IsRequired();

            // 2. publishers-master Table Mapping
            modelBuilder.Entity<PublisherMaster>().ToTable("publishers-master");
            modelBuilder.Entity<PublisherMaster>().HasKey(p => p.PId);
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PId).HasColumnName("p_id");
            modelBuilder.Entity<PublisherMaster>().Property(p => p.PName).HasColumnName("p_name").IsRequired();

            // 3. books-master Table Mapping
            modelBuilder.Entity<BookMaster>().ToTable("books-master");
            modelBuilder.Entity<BookMaster>().HasKey(b => b.BookId);
            modelBuilder.Entity<BookMaster>().Property(b => b.BookId).HasColumnName("book_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.Title).HasColumnName("title").IsRequired();
            modelBuilder.Entity<BookMaster>().Property(b => b.CId).HasColumnName("c_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.PId).HasColumnName("p_id");
            modelBuilder.Entity<BookMaster>().Property(b => b.Isbn).HasColumnName("isbn");
            modelBuilder.Entity<BookMaster>().HasIndex(b => b.Isbn).IsUnique().HasFilter("[isbn] IS NOT NULL");

            // 4. Bookscopies Table Mapping
            modelBuilder.Entity<BookCopy>().ToTable("Bookscopies");
            modelBuilder.Entity<BookCopy>().HasKey(bc => bc.BookCopyId);

            // FIXED: Added .UseIdentityColumn(1001, 1) to seed the ID at 1001
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
        }
    }
}