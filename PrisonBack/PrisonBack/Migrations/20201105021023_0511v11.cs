using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class _0511v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Forname = table.Column<string>(nullable: true),
                    Login = table.Column<string>(maxLength: 24, nullable: true),
                    Password = table.Column<string>(maxLength: 24, nullable: true),
                    Mail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IdPermission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermission_Permission_IdPermission",
                        column: x => x.IdPermission,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_UsersList_IdUser",
                        column: x => x.IdUser,
                        principalTable: "UsersList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "UsersList",
                columns: new[] { "Id", "Forname", "Login", "Mail", "Name", "Password" },
                values: new object[] { 100, null, "Bartek", "blabla", null, "xxxx" });

            migrationBuilder.InsertData(
                table: "UsersList",
                columns: new[] { "Id", "Forname", "Login", "Mail", "Name", "Password" },
                values: new object[] { 101, null, "Bartek1", "blabla", null, "xxxxxx" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_IdPermission",
                table: "UserPermission",
                column: "IdPermission");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_IdUser",
                table: "UserPermission",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "UsersList");
        }
    }
}
