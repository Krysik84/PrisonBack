using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class pass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prisoner_Prison_IdPrisoner",
                table: "Prisoner");

            migrationBuilder.DropIndex(
                name: "IX_Prisoner_IdPrisoner",
                table: "Prisoner");

            migrationBuilder.DropColumn(
                name: "IdPrison",
                table: "Prisoner");

            migrationBuilder.DropColumn(
                name: "IdPrisoner",
                table: "Prisoner");

            migrationBuilder.AddColumn<int>(
                name: "PrisonId",
                table: "Prisoner",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Isolation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IdPrisoner = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isolation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Isolation_Prisoner_IdPrisoner",
                        column: x => x.IdPrisoner,
                        principalTable: "Prisoner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    IdPrisoner = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pass_Prisoner_IdPrisoner",
                        column: x => x.IdPrisoner,
                        principalTable: "Prisoner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pass_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_PrisonId",
                table: "Prisoner",
                column: "PrisonId");

            migrationBuilder.CreateIndex(
                name: "IX_Isolation_IdPrisoner",
                table: "Isolation",
                column: "IdPrisoner");

            migrationBuilder.CreateIndex(
                name: "IX_Pass_IdPrisoner",
                table: "Pass",
                column: "IdPrisoner");

            migrationBuilder.CreateIndex(
                name: "IX_Pass_IdUser",
                table: "Pass",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Prisoner_Prison_PrisonId",
                table: "Prisoner",
                column: "PrisonId",
                principalTable: "Prison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prisoner_Prison_PrisonId",
                table: "Prisoner");

            migrationBuilder.DropTable(
                name: "Isolation");

            migrationBuilder.DropTable(
                name: "Pass");

            migrationBuilder.DropIndex(
                name: "IX_Prisoner_PrisonId",
                table: "Prisoner");

            migrationBuilder.DropColumn(
                name: "PrisonId",
                table: "Prisoner");

            migrationBuilder.AddColumn<int>(
                name: "IdPrison",
                table: "Prisoner",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPrisoner",
                table: "Prisoner",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_IdPrisoner",
                table: "Prisoner",
                column: "IdPrisoner");

            migrationBuilder.AddForeignKey(
                name: "FK_Prisoner_Prison_IdPrisoner",
                table: "Prisoner",
                column: "IdPrisoner",
                principalTable: "Prison",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
