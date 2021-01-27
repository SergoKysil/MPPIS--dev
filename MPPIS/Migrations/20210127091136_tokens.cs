using Microsoft.EntityFrameworkCore.Migrations;

namespace MPPIS.Migrations
{
    public partial class tokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TokenRefresh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenRefresh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenRefresh_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAEFw+rTIwRCL5oz56L3HkEDdQbKiuyNDd7TJ7uZriAqfezgzMBWC8vWcC5G+tjwHTWg==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAEHVvDS3fEyq1+u+m7+aXCO7NV4gKVfYvCUZq37okoSgKS9WZvojQD2VQY6bg+4tMDQ==");

            migrationBuilder.CreateIndex(
                name: "IX_TokenRefresh_UserId",
                table: "TokenRefresh",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokenRefresh");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAEBpfzjFWaAeBZH5DxPLt+ez4ZCGi1QdjrN0iQ3IwmBeMFcNr+mvu6NN2XurJ7MzZyw==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAEFpTyZWWFZghh7YywPde4VaDz7DLsjxStL1VpSk6lkecCLI8Amd5b5AqvIwkGdMLFw==");
        }
    }
}
