using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    public partial class AddReturnTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- COMMENTED OUT: ALREADY EXISTS IN DB ---
            // migrationBuilder.CreateSequence<int>(name: "IssueSeq");

            migrationBuilder.CreateSequence<int>(
                name: "ReturnDetailSeq");

            migrationBuilder.CreateSequence<int>(
                name: "ReturnSeq");

            // --- COMMENTED OUT: ALREADY EXISTS IN DB ---
            // migrationBuilder.CreateTable(name: "issue-master", ... );
            // migrationBuilder.CreateTable(name: "issue-detail-master", ... );

            migrationBuilder.CreateTable(
                name: "return-master",
                columns: table => new
                {
                    r_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('R', NEXT VALUE FOR ReturnSeq)"),
                    s_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_return-master", x => x.r_id);
                    table.ForeignKey(
                        name: "FK_return-master_students-master_s_id",
                        column: x => x.s_id,
                        principalTable: "students-master",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "return-detail-master",
                columns: table => new
                {
                    return_detail_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('RD', NEXT VALUE FOR ReturnDetailSeq)"),
                    r_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    issue_detail_id = table.Column<int>(type: "int", nullable: false),
                    condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_return-detail-master", x => x.return_detail_id);
                    table.ForeignKey(
                        name: "FK_return-detail-master_issue-detail-master_issue_detail_id",
                        column: x => x.issue_detail_id,
                        principalTable: "issue-detail-master",
                        principalColumn: "issue_detail_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_return-detail-master_return-master_r_id",
                        column: x => x.r_id,
                        principalTable: "return-master",
                        principalColumn: "r_id",
                        onDelete: ReferentialAction.Cascade);
                });

            // --- COMMENTED OUT: ALREADY EXISTS IN DB ---
            // migrationBuilder.CreateIndex(name: "IX_issue-detail-master_book_copy_id", ...);
            // migrationBuilder.CreateIndex(name: "IX_issue-detail-master_issue_id", ...);
            // migrationBuilder.CreateIndex(name: "IX_issue-master_s_id", ...);

            migrationBuilder.CreateIndex(
                name: "IX_return-detail-master_issue_detail_id",
                table: "return-detail-master",
                column: "issue_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_return-detail-master_r_id",
                table: "return-detail-master",
                column: "r_id");

            migrationBuilder.CreateIndex(
                name: "IX_return-master_s_id",
                table: "return-master",
                column: "s_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "return-detail-master");
            migrationBuilder.DropTable(name: "return-master");
            migrationBuilder.DropSequence(name: "ReturnDetailSeq");
            migrationBuilder.DropSequence(name: "ReturnSeq");
        }
    }
}