using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class AddIssueManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create the Sequence for Issue IDs
            migrationBuilder.CreateSequence<int>(
                name: "IssueSeq");

            // 2. Create the Issue Master Table
            migrationBuilder.CreateTable(
                name: "issue-master",
                columns: table => new
                {
                    issue_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('i', NEXT VALUE FOR IssueSeq)"),
                    s_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    issue_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue-master", x => x.issue_id);
                    table.ForeignKey(
                        name: "FK_issue-master_students-master_s_id",
                        column: x => x.s_id,
                        principalTable: "students-master",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Restrict);
                });

            // 3. Create the Issue Detail Master Table
            migrationBuilder.CreateTable(
                name: "issue-detail-master",
                columns: table => new
                {
                    issue_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "3001, 1"),
                    issue_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    book_copy_id = table.Column<int>(type: "int", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue-detail-master", x => x.issue_detail_id);
                    table.ForeignKey(
                        name: "FK_issue-detail-master_Bookscopies_book_copy_id",
                        column: x => x.book_copy_id,
                        principalTable: "Bookscopies",
                        principalColumn: "book_copy_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_issue-detail-master_issue-master_issue_id",
                        column: x => x.issue_id,
                        principalTable: "issue-master",
                        principalColumn: "issue_id",
                        onDelete: ReferentialAction.Cascade);
                });

            // 4. Create Indexes
            migrationBuilder.CreateIndex(
                name: "IX_issue-detail-master_book_copy_id",
                table: "issue-detail-master",
                column: "book_copy_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue-detail-master_issue_id",
                table: "issue-detail-master",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue-master_s_id",
                table: "issue-master",
                column: "s_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue-detail-master");

            migrationBuilder.DropTable(
                name: "issue-master");

            migrationBuilder.DropSequence(
                name: "IssueSeq");
        }
    }
}