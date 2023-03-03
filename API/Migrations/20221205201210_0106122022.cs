using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREL_BE.Migrations
{
    public partial class _0106122022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentTerm",
                table: "Contract",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "BrandRepresentativeAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BrandRepresentativeBirthday",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandRepresentativeCccd",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandRepresentativeCccdGrantAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BrandRepresentativeCccdGrantDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandRepresentativeName",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandRepresentativePhoneNumber",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HandoverDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LeaseLength",
                table: "Contract",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessorAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessorBank",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessorBankAccountNumber",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LessorBirthDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessorCccd",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessorCccdGrantAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LessorCccdGrantDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayDay",
                table: "Contract",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumberGrantAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationNumberGrantDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RenterOfficeAddress",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignDate",
                table: "Contract",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BrandRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BrandRequestProperty",
                columns: table => new
                {
                    BrandRequestsId = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PropertiesId = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandRequestProperty", x => new { x.BrandRequestsId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_BrandRequestProperty_BrandRequest_BrandRequestsId",
                        column: x => x.BrandRequestsId,
                        principalTable: "BrandRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandRequestProperty_Property_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractTerm",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ContractTermID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTerm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractTerm_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ContractTerm_ContractTerm_ContractTermID",
                        column: x => x.ContractTermID,
                        principalTable: "ContractTerm",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrandRequestProperty_PropertiesId",
                table: "BrandRequestProperty",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTerm_ContractID",
                table: "ContractTerm",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTerm_ContractTermID_ContractID",
                table: "ContractTerm",
                columns: new[] { "ContractTermID", "ContractID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandRequestProperty");

            migrationBuilder.DropTable(
                name: "ContractTerm");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeBirthday",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeCccd",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeCccdGrantAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeCccdGrantDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativeName",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "BrandRepresentativePhoneNumber",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "HandoverDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LeaseLength",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorBank",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorBankAccountNumber",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorBirthDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorCccd",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorCccdGrantAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "LessorCccdGrantDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "PayDay",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "RegistrationNumberGrantAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "RegistrationNumberGrantDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "RenterOfficeAddress",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "SignDate",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BrandRequest");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentTerm",
                table: "Contract",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
