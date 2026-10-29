using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class SyncStudentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create the counting sequence for Departments (D1, D2, D3...)
            migrationBuilder.CreateSequence<int>(
                name: "DepartmentSeq");

            // 2. Create the counting sequence for Students (S1, S2, S3...)
            migrationBuilder.CreateSequence<int>(
                name: "StudentSeq");

            // All CreateTable commands are removed so it does not overwrite your existing SQL tables.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Since we didn't create tables in the Up method, we only drop the sequences here.
            migrationBuilder.DropSequence(
                name: "DepartmentSeq");

            migrationBuilder.DropSequence(
                name: "StudentSeq");
        }
    }
}