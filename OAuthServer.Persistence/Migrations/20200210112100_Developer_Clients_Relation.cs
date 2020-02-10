using Microsoft.EntityFrameworkCore.Migrations;

namespace OAuthServer.Persistence.Migrations
{
    public partial class Developer_Clients_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_User_Id",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Clients",
                newName: "Developer_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_User_Id",
                table: "Clients",
                newName: "IX_Clients_Developer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_Developer_Id",
                table: "Clients",
                column: "Developer_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Users_Developer_Id",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Developer_Id",
                table: "Clients",
                newName: "User_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_Developer_Id",
                table: "Clients",
                newName: "IX_Clients_User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Users_User_Id",
                table: "Clients",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
