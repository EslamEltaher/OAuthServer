using Microsoft.EntityFrameworkCore.Migrations;

namespace OAuthServer.Persistence.Migrations
{
    public partial class adding_RefreshToken_in_consent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Consents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Consents");
        }
    }
}
