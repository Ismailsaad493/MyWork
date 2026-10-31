using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class AddBookAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "BookAvailabilitySeq");

            migrationBuilder.CreateTable(
                name: "book-availability-master",
                columns: table => new
                {
                    availability_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('BA', NEXT VALUE FOR BookAvailabilitySeq)"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_copies = table.Column<int>(type: "int", nullable: false),
                    available_copies = table.Column<int>(type: "int", nullable: false),
                    issued_copies = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book-availability-master", x => x.availability_id);
                    table.ForeignKey(
                        name: "FK_book-availability-master_books-master_book_id",
                        column: x => x.book_id,
                        principalTable: "books-master",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_book-availability-master_book_id",
                table: "book-availability-master",
                column: "book_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book-availability-master");

            migrationBuilder.DropSequence(
                name: "BookAvailabilitySeq");
        }
    }
}
