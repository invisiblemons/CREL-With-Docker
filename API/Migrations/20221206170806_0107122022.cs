using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREL_BE.Migrations
{
    public partial class _0107122022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lessor",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lessor",
                table: "Contract");
        }
    }
}
