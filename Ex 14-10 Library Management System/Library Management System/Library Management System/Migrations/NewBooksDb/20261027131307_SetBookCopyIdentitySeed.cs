using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    public partial class SetBookCopyIdentitySeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Sets the current identity to 1000, making the NEXT inserted row exactly 1001
            migrationBuilder.Sql("DBCC CHECKIDENT ('Bookscopies', RESEED, 1000);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert back to 0 so the next row would be 1 (if you ever rollback this migration)
            migrationBuilder.Sql("DBCC CHECKIDENT ('Bookscopies', RESEED, 0);");
        }
    }
}