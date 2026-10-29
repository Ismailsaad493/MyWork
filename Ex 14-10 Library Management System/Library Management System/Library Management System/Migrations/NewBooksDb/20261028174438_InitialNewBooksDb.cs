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
