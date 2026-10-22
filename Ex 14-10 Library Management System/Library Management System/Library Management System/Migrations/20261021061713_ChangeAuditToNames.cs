using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAuditToNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Users_CreatedById",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Users_UpdatedById",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_CreatedById",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UpdatedById",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_UpdatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Users_CreatedById",
                table: "Fines");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Users_UpdatedById",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_CreatedById",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Fines_UpdatedById",
                table: "Fines");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Books_CreatedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UpdatedById",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_CreatedById",
                table: "BookLoans");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_UpdatedById",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "BookLoans");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookLoans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "BookLoans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BookLoans");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Fines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Fines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "BookLoans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "BookLoans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CreatedById",
                table: "Fines",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_UpdatedById",
                table: "Fines",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedById",
                table: "Categories",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedById",
                table: "Books",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Books_UpdatedById",
                table: "Books",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_CreatedById",
                table: "BookLoans",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_UpdatedById",
                table: "BookLoans",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Users_CreatedById",
                table: "BookLoans",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Users_UpdatedById",
                table: "BookLoans",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_CreatedById",
                table: "Books",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UpdatedById",
                table: "Books",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_UpdatedById",
                table: "Categories",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Users_CreatedById",
                table: "Fines",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Users_UpdatedById",
                table: "Fines",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
