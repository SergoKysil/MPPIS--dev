using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MPPIS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayPrice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0m),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPrice", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    district = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    vilage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    house_number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RouteDay",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    route = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDay", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    middlename = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    passwordhash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    registered_date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    is_email_confirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    location_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Location_location_id",
                        column: x => x.location_id,
                        principalTable: "Location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorageData",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_added = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    count_production = table.Column<decimal>(type: "decimal", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    day_price_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageData", x => x.id);
                    table.ForeignKey(
                        name: "FK_StorageData_DayPrice_day_price_id",
                        column: x => x.day_price_id,
                        principalTable: "DayPrice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorageData_User_user_id",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "id", "city", "district", "house_number", "street", "vilage" },
                values: new object[,]
                {
                    { 1, "Lviv", "Mostisky", "53", "Sagaydachnogo", "Tvirzha" },
                    { 2, "Lviv", "Mostisky", "53", "Sagaydachnogo", "Tvirzha" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "email", "firstname", "IsDeleted", "lastname", "location_id", "middlename", "passwordhash", "role_id" },
                values: new object[] { 1, "test@gmail.com", "Tester", false, "Testerovich", 1, "Test", "AQAAAAEAACcQAAAAEDTdfEPFFwBHqyumL0F/6uizxeQzWUyXULkpfOFAmids2PVYIVyIu18GhZf4S/Rc3g==", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "email", "firstname", "IsDeleted", "lastname", "location_id", "middlename", "passwordhash", "role_id" },
                values: new object[] { 2, "admin@gmail.com", "Admin", false, "Adminovich", 2, "Adminovski", "AQAAAAEAACcQAAAAENd1CHKu9xVrSBrFkGWBIRQ1xaTo4EPGF8pBCt+0G+xaax5YAu4MX0MS0Do2XYGUiQ==", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_StorageData_day_price_id",
                table: "StorageData",
                column: "day_price_id");

            migrationBuilder.CreateIndex(
                name: "IX_StorageData_user_id",
                table: "StorageData",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_location_id",
                table: "User",
                column: "location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteDay");

            migrationBuilder.DropTable(
                name: "StorageData");

            migrationBuilder.DropTable(
                name: "DayPrice");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
