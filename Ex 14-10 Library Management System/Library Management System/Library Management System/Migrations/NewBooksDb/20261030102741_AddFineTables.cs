using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class AddFineTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "FineDetailSeq");

            migrationBuilder.CreateSequence<int>(
                name: "FineSeq");

            migrationBuilder.CreateTable(
                name: "fine-master",
                columns: table => new
                {
                    fine_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('F', NEXT VALUE FOR FineSeq)"),
                    issue_detail_id = table.Column<int>(type: "int", nullable: false),
                    fine_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fine_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_fine-master", x => x.fine_id);
                    table.ForeignKey(
                        name: "FK_fine-master_issue-detail-master_issue_detail_id",
                        column: x => x.issue_detail_id,
                        principalTable: "issue-detail-master",
                        principalColumn: "issue_detail_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fine-detail-master",
                columns: table => new
                {
                    fine_detail_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('FD', NEXT VALUE FOR FineDetailSeq)"),
                    fine_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    charge_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fine-detail-master", x => x.fine_detail_id);
                    table.ForeignKey(
                        name: "FK_fine-detail-master_fine-master_fine_id",
                        column: x => x.fine_id,
                        principalTable: "fine-master",
                        principalColumn: "fine_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_fine-detail-master_fine_id",
                table: "fine-detail-master",
                column: "fine_id");

            migrationBuilder.CreateIndex(
                name: "IX_fine-master_issue_detail_id",
                table: "fine-master",
                column: "issue_detail_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fine-detail-master");

            migrationBuilder.DropTable(
                name: "fine-master");

            migrationBuilder.DropSequence(
                name: "FineDetailSeq");

            migrationBuilder.DropSequence(
                name: "FineSeq");
        }
    }
}
