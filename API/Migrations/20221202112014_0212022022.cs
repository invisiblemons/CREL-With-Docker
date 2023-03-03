using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREL_BE.Migrations
{
    public partial class _0212022022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfFrontage",
                table: "Property",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfFrontage",
                table: "Property");
        }
    }
}
