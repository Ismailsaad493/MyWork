using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class SplitUserAndStudentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Students_UserId",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Students_UserId",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Fines");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Fines",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Fines_UserId",
                table: "Fines",
                newName: "IX_Fines_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BookLoans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "BookLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_StudentId",
                table: "BookLoans",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Students_StudentId",
                table: "BookLoans",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Users_UserId",
                table: "BookLoans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Students_StudentId",
                table: "Fines",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Students_StudentId",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Users_UserId",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_Fines_Students_StudentId",
                table: "Fines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_StudentId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "BookLoans");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Fines",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Fines_StudentId",
                table: "Fines",
                newName: "IX_Fines_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateCount",
                table: "Fines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Fines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BookLoans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Students_UserId",
                table: "BookLoans",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fines_Students_UserId",
                table: "Fines",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
