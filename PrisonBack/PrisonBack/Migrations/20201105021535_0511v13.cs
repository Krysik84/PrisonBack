using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class _0511v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserPermission",
                columns: new[] { "Id", "IdPermission", "IdUser" },
                values: new object[] { 1, 1, 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserPermission",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
