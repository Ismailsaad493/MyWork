using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeAndPrecisionClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateCount",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BookLoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateCount",
                table: "BookLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BookLoans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_UserId",
                table: "BookLoans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Books_BookId",
                table: "BookLoans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Users_UserId",
                table: "BookLoans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Books_BookId",
                table: "BookLoans");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Users_UserId",
                table: "BookLoans");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_UserId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "UpdateCount",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BookLoans");
        }
    }
}
