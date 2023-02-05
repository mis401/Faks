using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brendovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brendovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Velicina = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true),
                    BrendID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Artikli_Brendovi_BrendID",
                        column: x => x.BrendID,
                        principalTable: "Brendovi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Artikli_Prodavnice_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_BrendID",
                table: "Artikli",
                column: "BrendID");

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_ProdavnicaID",
                table: "Artikli",
                column: "ProdavnicaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Brendovi");

            migrationBuilder.DropTable(
                name: "Prodavnice");
        }
    }
}
