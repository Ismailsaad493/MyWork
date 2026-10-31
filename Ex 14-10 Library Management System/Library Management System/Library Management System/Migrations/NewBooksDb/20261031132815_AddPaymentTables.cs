using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations.NewBooksDb
{
    /// <inheritdoc />
    public partial class AddPaymentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "PaymentDetailSeq");

            migrationBuilder.CreateSequence<int>(
                name: "PaymentSeq");

            migrationBuilder.CreateTable(
                name: "payment-master",
                columns: table => new
                {
                    payment_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('P', NEXT VALUE FOR PaymentSeq)"),
                    s_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment-master", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_payment-master_students-master_s_id",
                        column: x => x.s_id,
                        principalTable: "students-master",
                        principalColumn: "s_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "payment-detail-master",
                columns: table => new
                {
                    payment_detail_id = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "CONCAT('PD', NEXT VALUE FOR PaymentDetailSeq)"),
                    payment_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fine_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    paid_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment-detail-master", x => x.payment_detail_id);
                    table.ForeignKey(
                        name: "FK_payment-detail-master_fine-master_fine_id",
                        column: x => x.fine_id,
                        principalTable: "fine-master",
                        principalColumn: "fine_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_payment-detail-master_payment-master_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payment-master",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payment-detail-master_fine_id",
                table: "payment-detail-master",
                column: "fine_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment-detail-master_payment_id",
                table: "payment-detail-master",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_payment-master_s_id",
                table: "payment-master",
                column: "s_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment-detail-master");

            migrationBuilder.DropTable(
                name: "payment-master");

            migrationBuilder.DropSequence(
                name: "PaymentDetailSeq");

            migrationBuilder.DropSequence(
                name: "PaymentSeq");
        }
    }
}
