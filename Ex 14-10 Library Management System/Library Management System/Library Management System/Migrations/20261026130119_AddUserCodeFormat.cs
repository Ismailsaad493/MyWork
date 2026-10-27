using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCodeFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "'LIB' + RIGHT('000' + CAST([Id] AS VARCHAR(10)), 3)",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCode",
                table: "Users");
        }
    }
}
