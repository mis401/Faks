using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProdukcijskeKuce",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdukcijskeKuce", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Filmovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategorijaID = table.Column<int>(type: "int", nullable: true),
                    ProdukcijskaKucaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Filmovi_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Filmovi_ProdukcijskeKuce_ProdukcijskaKucaID",
                        column: x => x.ProdukcijskaKucaID,
                        principalTable: "ProdukcijskeKuce",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "KategorijaProdukcijskaKuca",
                columns: table => new
                {
                    ListaKategorijaID = table.Column<int>(type: "int", nullable: false),
                    ListaKucaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategorijaProdukcijskaKuca", x => new { x.ListaKategorijaID, x.ListaKucaID });
                    table.ForeignKey(
                        name: "FK_KategorijaProdukcijskaKuca_Kategorije_ListaKategorijaID",
                        column: x => x.ListaKategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KategorijaProdukcijskaKuca_ProdukcijskeKuce_ListaKucaID",
                        column: x => x.ListaKucaID,
                        principalTable: "ProdukcijskeKuce",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocene",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmID = table.Column<int>(type: "int", nullable: true),
                    Suma = table.Column<int>(type: "int", nullable: false),
                    BrojOcena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocene", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocene_Filmovi_FilmID",
                        column: x => x.FilmID,
                        principalTable: "Filmovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmovi_KategorijaID",
                table: "Filmovi",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Filmovi_ProdukcijskaKucaID",
                table: "Filmovi",
                column: "ProdukcijskaKucaID");

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProdukcijskaKuca_ListaKucaID",
                table: "KategorijaProdukcijskaKuca",
                column: "ListaKucaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocene_FilmID",
                table: "Ocene",
                column: "FilmID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategorijaProdukcijskaKuca");

            migrationBuilder.DropTable(
                name: "Ocene");

            migrationBuilder.DropTable(
                name: "Filmovi");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "ProdukcijskeKuce");
        }
    }
}
