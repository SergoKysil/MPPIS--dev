using Microsoft.EntityFrameworkCore.Migrations;

namespace MPPIS.Migrations
{
    public partial class newone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 1,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAEDTdfEPFFwBHqyumL0F/6uizxeQzWUyXULkpfOFAmids2PVYIVyIu18GhZf4S/Rc3g==");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "id",
                keyValue: 2,
                column: "passwordhash",
                value: "AQAAAAEAACcQAAAAENd1CHKu9xVrSBrFkGWBIRQ1xaTo4EPGF8pBCt+0G+xaax5YAu4MX0MS0Do2XYGUiQ==");
        }
    }
}
