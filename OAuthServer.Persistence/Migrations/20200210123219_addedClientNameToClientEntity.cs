using Microsoft.EntityFrameworkCore.Migrations;

namespace OAuthServer.Persistence.Migrations
{
    public partial class addedClientNameToClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Client_Name",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client_Name",
                table: "Clients");
        }
    }
}
