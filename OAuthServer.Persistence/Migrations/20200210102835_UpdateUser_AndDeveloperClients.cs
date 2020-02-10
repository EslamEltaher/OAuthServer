using Microsoft.EntityFrameworkCore.Migrations;

namespace OAuthServer.Persistence.Migrations
{
    public partial class UpdateUser_AndDeveloperClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_User_Id",
                table: "Clients",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_User_Id",
                table: "Clients",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_User_Id",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_User_Id",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Clients");
        }
    }
}
