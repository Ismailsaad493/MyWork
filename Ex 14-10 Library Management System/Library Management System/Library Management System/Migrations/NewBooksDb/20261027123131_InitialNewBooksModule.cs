using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class InitialNewBooksModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories-master",
                columns: table => new
                {
                    c_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories-master", x => x.c_id);
                });

            migrationBuilder.CreateTable(
                name: "publishers-master",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers-master", x => x.p_id);
                });

            migrationBuilder.CreateTable(
                name: "books-master",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isbn = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    c_id = table.Column<int>(type: "int", nullable: false),
                    p_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books-master", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_books-master_categories-master_c_id",
                        column: x => x.c_id,
                        principalTable: "categories-master",
                        principalColumn: "c_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_books-master_publishers-master_p_id",
                        column: x => x.p_id,
                        principalTable: "publishers-master",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookscopies",
                columns: table => new
                {
                    book_copy_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookscopies", x => x.book_copy_id);
                    table.ForeignKey(
                        name: "FK_Bookscopies_books-master_book_id",
                        column: x => x.book_id,
                        principalTable: "books-master",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books-master_c_id",
                table: "books-master",
                column: "c_id");

            migrationBuilder.CreateIndex(
                name: "IX_books-master_isbn",
                table: "books-master",
                column: "isbn",
                unique: true,
                filter: "[isbn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_books-master_p_id",
                table: "books-master",
                column: "p_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookscopies_book_id",
                table: "Bookscopies",
                column: "book_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookscopies");

            migrationBuilder.DropTable(
                name: "books-master");

            migrationBuilder.DropTable(
                name: "categories-master");

            migrationBuilder.DropTable(
                name: "publishers-master");
        }
    }
}
