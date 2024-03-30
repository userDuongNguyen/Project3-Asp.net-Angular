using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivingDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facility = table.Column<int>(type: "int", nullable: true),
                    PetAllowance = table.Column<int>(type: "int", nullable: true),
                    HeatingType = table.Column<int>(type: "int", nullable: true),
                    BathroomNumber = table.Column<int>(type: "int", nullable: false),
                    BedroomNumber = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    MaxFloor = table.Column<int>(type: "int", nullable: false),
                    UsableArea = table.Column<float>(type: "real", nullable: false),
                    ParkingNumber = table.Column<int>(type: "int", nullable: false),
                    ParkingRent = table.Column<float>(type: "real", nullable: false),
                    ParkingType = table.Column<int>(type: "int", nullable: true),
                    InternetSpeed = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salutation = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalFee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasicRent = table.Column<float>(type: "real", nullable: false),
                    ExtraCost = table.Column<float>(type: "real", nullable: false),
                    Deposit = table.Column<float>(type: "real", nullable: false),
                    HeatingCost = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyType = table.Column<int>(type: "int", nullable: true),
                    VacantTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    AccommodationDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_AccommodationDetail_AccommodationDetailId",
                        column: x => x.AccommodationDetailId,
                        principalTable: "AccommodationDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Property_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agent_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agent_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactDetailId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentRole = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: false),
                    AgentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_ContactDetail_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Headline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandSize = table.Column<float>(type: "real", nullable: false),
                    RoomNumber = table.Column<float>(type: "real", nullable: false),
                    EfficiencyClass = table.Column<int>(type: "int", nullable: true),
                    AccommodationType = table.Column<int>(type: "int", nullable: false),
                    ResidenceCertificate = table.Column<int>(type: "int", nullable: false),
                    ContactDetail = table.Column<int>(type: "int", nullable: true),
                    Property = table.Column<int>(type: "int", nullable: true),
                    RentalFee = table.Column<int>(type: "int", nullable: true),
                    User = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Listing_ContactDetail_ContactDetail",
                        column: x => x.ContactDetail,
                        principalTable: "ContactDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listing_Property_Property",
                        column: x => x.Property,
                        principalTable: "Property",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listing_RentalFee_RentalFee",
                        column: x => x.RentalFee,
                        principalTable: "RentalFee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Listing_User_User",
                        column: x => x.User,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subcription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcriptionPack = table.Column<int>(type: "int", nullable: true),
                    SubcriptionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubcriptionDuration = table.Column<int>(type: "int", nullable: false),
                    SubcriptionType = table.Column<int>(type: "int", nullable: true),
                    Subcription = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcription_User_Subcription",
                        column: x => x.Subcription,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AddressId",
                table: "Agent",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_WalletId",
                table: "Agent",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_ContactDetail",
                table: "Listing",
                column: "ContactDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_Property",
                table: "Listing",
                column: "Property");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_RentalFee",
                table: "Listing",
                column: "RentalFee");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_User",
                table: "Listing",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Property_AccommodationDetailId",
                table: "Property",
                column: "AccommodationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_AddressId",
                table: "Property",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcription_Subcription",
                table: "Subcription",
                column: "Subcription");

            migrationBuilder.CreateIndex(
                name: "IX_User_AgentId",
                table: "User",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_ContactDetailId",
                table: "User",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WalletId",
                table: "User",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessage");

            migrationBuilder.DropTable(
                name: "Listing");

            migrationBuilder.DropTable(
                name: "Subcription");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "RentalFee");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AccommodationDetail");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "ContactDetail");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Wallet");
        }
    }
}
