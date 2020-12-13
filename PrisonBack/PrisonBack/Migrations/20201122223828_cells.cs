using Microsoft.EntityFrameworkCore.Migrations;

namespace PrisonBack.Migrations
{
    public partial class cells : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CellType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prison",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrisonName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prison", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cell",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Beds = table.Column<int>(nullable: false),
                    IdPrison = table.Column<int>(nullable: false),
                    IdCellType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cell_CellType_IdCellType",
                        column: x => x.IdCellType,
                        principalTable: "CellType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cell_Prison_IdPrison",
                        column: x => x.IdPrison,
                        principalTable: "Prison",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cell_IdCellType",
                table: "Cell",
                column: "IdCellType");

            migrationBuilder.CreateIndex(
                name: "IX_Cell_IdPrison",
                table: "Cell",
                column: "IdPrison");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cell");

            migrationBuilder.DropTable(
                name: "CellType");

            migrationBuilder.DropTable(
                name: "Prison");
        }
    }
}
