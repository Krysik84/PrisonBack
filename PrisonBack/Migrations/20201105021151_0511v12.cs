using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class _0511v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_UsersList_IdUser",
                table: "UserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersList",
                table: "UsersList");

            migrationBuilder.RenameTable(
                name: "UsersList",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "User",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_User_IdUser",
                table: "UserPermission",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_User_IdUser",
                table: "UserPermission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UsersList");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UsersList",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "Mail",
                table: "UsersList",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "UsersList",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 24);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersList",
                table: "UsersList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_UsersList_IdUser",
                table: "UserPermission",
                column: "IdUser",
                principalTable: "UsersList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
