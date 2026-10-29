using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class InitialNewBooksDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "publishers-master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "publishers-master",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "publishers-master",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "update_count",
                table: "publishers-master",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "publishers-master",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "publishers-master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "categories-master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "categories-master",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "categories-master",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "update_count",
                table: "categories-master",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "categories-master",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "categories-master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "Bookscopies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "Bookscopies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "Bookscopies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "update_count",
                table: "Bookscopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Bookscopies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "Bookscopies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "books-master",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_time",
                table: "books-master",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "books-master",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "update_count",
                table: "books-master",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "books-master",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "books-master",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "update_count",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "publishers-master");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "update_count",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "categories-master");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "update_count",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "Bookscopies");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "books-master");

            migrationBuilder.DropColumn(
                name: "created_time",
                table: "books-master");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "books-master");

            migrationBuilder.DropColumn(
                name: "update_count",
                table: "books-master");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "books-master");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "books-master");
        }
    }
}
