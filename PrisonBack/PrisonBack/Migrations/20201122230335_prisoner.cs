using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class prisoner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prisoner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Forname = table.Column<string>(nullable: true),
                    Pesel = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Pass = table.Column<bool>(nullable: false),
                    Behavior = table.Column<int>(nullable: false),
                    Isolated = table.Column<bool>(nullable: false),
                    IdCell = table.Column<int>(nullable: false),
                    IdPrison = table.Column<int>(nullable: false),
                    IdPrisoner = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prisoner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prisoner_Cell_IdCell",
                        column: x => x.IdCell,
                        principalTable: "Cell",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prisoner_Prison_IdPrisoner",
                        column: x => x.IdPrisoner,
                        principalTable: "Prison",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_IdCell",
                table: "Prisoner",
                column: "IdCell");

            migrationBuilder.CreateIndex(
                name: "IX_Prisoner_IdPrisoner",
                table: "Prisoner",
                column: "IdPrisoner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prisoner");
        }
    }
}
