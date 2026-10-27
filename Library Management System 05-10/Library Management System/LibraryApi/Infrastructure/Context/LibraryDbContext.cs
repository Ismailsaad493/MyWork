using LibraryApi.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Context;

public sealed class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Fine> Fines => Set<Fine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(book => book.Id);
            entity.Property(book => book.Title).HasMaxLength(200).IsRequired();
            entity.Property(book => book.Author).HasMaxLength(150).IsRequired();
            entity.Property(book => book.IsAvailable).HasDefaultValue(true);
            entity.Property(book => book.RowVersion).IsRowVersion();
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Members");
            entity.HasKey(member => member.Id);
            entity.Property(member => member.FullName).HasMaxLength(150).IsRequired();
            entity.Property(member => member.Email).HasMaxLength(254).IsRequired();
            entity.HasIndex(member => member.Email).IsUnique();
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.ToTable("Loans");
            entity.HasKey(loan => loan.Id);
            entity.Property(loan => loan.BorrowedAtUtc).IsRequired();
            entity.Property(loan => loan.DueAtUtc).IsRequired();
            entity.HasIndex(loan => loan.BookId)
                .IsUnique()
                .HasFilter("[ReturnedAtUtc] IS NULL");
            entity.HasIndex(loan => loan.MemberId);
            entity.HasOne(loan => loan.Book)
                .WithMany(book => book.Loans)
                .HasForeignKey(loan => loan.BookId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(loan => loan.Member)
                .WithMany(member => member.Loans)
                .HasForeignKey(loan => loan.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.ToTable("Fines");
            entity.HasKey(fine => fine.Id);
            entity.Property(fine => fine.FineType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();
            entity.Property(fine => fine.Amount).HasPrecision(18, 2).IsRequired();
            entity.Property(fine => fine.CreatedAtUtc).IsRequired();
            entity.Property(fine => fine.PaymentDate).HasColumnType("date");
            entity.Property(fine => fine.PaymentMethod).HasMaxLength(10);
            entity.HasIndex(fine => new { fine.BookLoanId, fine.FineType }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
