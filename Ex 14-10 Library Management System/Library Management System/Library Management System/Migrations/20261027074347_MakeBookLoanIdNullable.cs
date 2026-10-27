using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MakeBookLoanIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_BookLoans_BookLoanId",
                table: "Fines");

            migrationBuilder.AlterColumn<int>(
                name: "BookLoanId",
                table: "Fines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_BookLoans_BookLoanId",
                table: "Fines",
                column: "BookLoanId",
                principalTable: "BookLoans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fines_BookLoans_BookLoanId",
                table: "Fines");

            migrationBuilder.AlterColumn<int>(
                name: "BookLoanId",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_BookLoans_BookLoanId",
                table: "Fines",
                column: "BookLoanId",
                principalTable: "BookLoans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
