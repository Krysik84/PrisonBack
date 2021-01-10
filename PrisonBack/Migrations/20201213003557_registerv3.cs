using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class registerv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_IdPrison",
                table: "UserPermission",
                column: "IdPrison");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Prison_IdPrison",
                table: "UserPermission",
                column: "IdPrison",
                principalTable: "Prison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Prison_IdPrison",
                table: "UserPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserPermission_IdPrison",
                table: "UserPermission");
        }
    }
}
