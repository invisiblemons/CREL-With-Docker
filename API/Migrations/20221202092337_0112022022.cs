using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CREL_BE.Migrations
{
    public partial class _0112022022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaManager",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AvatarFileID = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    AvatarLink = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaManager", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistrictID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    HandoverYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_District_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreetSegment",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DistrictID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetSegment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StreetSegment_District_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirebaseUID = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    Email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RejectMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AvatarFileID = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    AvatarLink = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    IndustryID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Brand_Industry_IndustryID",
                        column: x => x.IndustryID,
                        principalTable: "Industry",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AreaManagerTeam",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    AreaManagerID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaManagerTeam", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AreaManagerTeam_AreaManager_AreaManagerID",
                        column: x => x.AreaManagerID,
                        principalTable: "AreaManager",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaManagerTeam_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Broker",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    AvatarFileID = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true),
                    AvatarLink = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Broker", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Broker_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TeamID = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ward_District_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ward_Team_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Team",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BrandRequest",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    AmountFrontage = table.Column<int>(type: "int", nullable: true),
                    MinPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    MinRentalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxRentalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MinFloorArea = table.Column<double>(type: "float", nullable: true),
                    MaxFloorArea = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BrandRequest_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Store_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WardID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StreetSegmentID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Location_StreetSegment_StreetSegmentID",
                        column: x => x.StreetSegmentID,
                        principalTable: "StreetSegment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Ward_WardID",
                        column: x => x.WardID,
                        principalTable: "Ward",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndustryLocation",
                columns: table => new
                {
                    IndustryID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    LocationID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndustryLocation", x => new { x.IndustryID, x.LocationID });
                    table.ForeignKey(
                        name: "FK_IndustryLocation_Industry_IndustryID",
                        column: x => x.IndustryID,
                        principalTable: "Industry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndustryLocation_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    BrokerID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectId = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Direction = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FloorArea = table.Column<double>(type: "float", nullable: true),
                    Area = table.Column<double>(type: "float", nullable: true),
                    Frontage = table.Column<double>(type: "float", nullable: true),
                    Certificates = table.Column<int>(type: "int", nullable: true),
                    Vertical = table.Column<double>(type: "float", nullable: true),
                    Horizontal = table.Column<double>(type: "float", nullable: true),
                    RoadWidth = table.Column<double>(type: "float", nullable: true),
                    RentalCondition = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RentalTerm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DepositTerm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PaymentTerm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Property_Broker_BrokerID",
                        column: x => x.BrokerID,
                        principalTable: "Broker",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Property_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Property_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    BrokerID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PropertyID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    OnDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appointment_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_Broker_BrokerID",
                        column: x => x.BrokerID,
                        principalTable: "Broker",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointment_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    OwnerID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PropertyID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    BrandID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    BrokerID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReasonCancel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentTerm = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contract_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Broker_BrokerID",
                        column: x => x.BrokerID,
                        principalTable: "Broker",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contract_Owner_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owner",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Contract_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerProperty",
                columns: table => new
                {
                    OwnersId = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PropertiesId = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerProperty", x => new { x.OwnersId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_OwnerProperty_Owner_OwnersId",
                        column: x => x.OwnersId,
                        principalTable: "Owner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerProperty_Property_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyBrand",
                columns: table => new
                {
                    BrandID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PropertyID = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyBrand", x => new { x.BrandID, x.PropertyID });
                    table.ForeignKey(
                        name: "FK_PropertyBrand_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyBrand_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false),
                    FileID = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false),
                    PropertyID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ProjectID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ContractID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Media_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Media_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Media_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_BrandID_BrokerID_PropertyID",
                table: "Appointment",
                columns: new[] { "BrandID", "BrokerID", "PropertyID" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_BrokerID",
                table: "Appointment",
                column: "BrokerID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PropertyID",
                table: "Appointment",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_AreaManagerTeam_AreaManagerID",
                table: "AreaManagerTeam",
                column: "AreaManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_AreaManagerTeam_TeamID_AreaManagerID",
                table: "AreaManagerTeam",
                columns: new[] { "TeamID", "AreaManagerID" });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IndustryID",
                table: "Brand",
                column: "IndustryID");

            migrationBuilder.CreateIndex(
                name: "IX_BrandRequest_BrandID",
                table: "BrandRequest",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Broker_TeamID",
                table: "Broker",
                column: "TeamID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BrandID_OwnerID_PropertyID_BrokerID",
                table: "Contract",
                columns: new[] { "BrandID", "OwnerID", "PropertyID", "BrokerID" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BrokerID",
                table: "Contract",
                column: "BrokerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_OwnerID",
                table: "Contract",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_PropertyID",
                table: "Contract",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_IndustryLocation_IndustryID_LocationID",
                table: "IndustryLocation",
                columns: new[] { "IndustryID", "LocationID" });

            migrationBuilder.CreateIndex(
                name: "IX_IndustryLocation_LocationID",
                table: "IndustryLocation",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_StreetSegmentID",
                table: "Location",
                column: "StreetSegmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_WardID_StreetSegmentID",
                table: "Location",
                columns: new[] { "WardID", "StreetSegmentID" });

            migrationBuilder.CreateIndex(
                name: "IX_Media_ContractID_ProjectID_PropertyID",
                table: "Media",
                columns: new[] { "ContractID", "ProjectID", "PropertyID" });

            migrationBuilder.CreateIndex(
                name: "IX_Media_ProjectID",
                table: "Media",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Media_PropertyID",
                table: "Media",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerProperty_PropertiesId",
                table: "OwnerProperty",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DistrictID",
                table: "Project",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_BrokerID_ProjectId_LocationID",
                table: "Property",
                columns: new[] { "BrokerID", "ProjectId", "LocationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Property_LocationID",
                table: "Property",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ProjectId",
                table: "Property",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyBrand_BrandID_PropertyID",
                table: "PropertyBrand",
                columns: new[] { "BrandID", "PropertyID" });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyBrand_PropertyID",
                table: "PropertyBrand",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Store_BrandID",
                table: "Store",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_StreetSegment_DistrictID",
                table: "StreetSegment",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_DistrictID",
                table: "Ward",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Ward_TeamID_DistrictID",
                table: "Ward",
                columns: new[] { "TeamID", "DistrictID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AreaManagerTeam");

            migrationBuilder.DropTable(
                name: "BrandRequest");

            migrationBuilder.DropTable(
                name: "IndustryLocation");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "OwnerProperty");

            migrationBuilder.DropTable(
                name: "PropertyBrand");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "AreaManager");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Broker");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "StreetSegment");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
