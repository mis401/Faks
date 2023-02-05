using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekat_Vezbe.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NepoznataPtica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NepoznataPtica", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Osobina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrednost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Podrucje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podrucje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NepoznataPticaOsobina",
                columns: table => new
                {
                    NepoznataID = table.Column<int>(type: "int", nullable: false),
                    OsobineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NepoznataPticaOsobina", x => new { x.NepoznataID, x.OsobineID });
                    table.ForeignKey(
                        name: "FK_NepoznataPticaOsobina_NepoznataPtica_NepoznataID",
                        column: x => x.NepoznataID,
                        principalTable: "NepoznataPtica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NepoznataPticaOsobina_Osobina_OsobineID",
                        column: x => x.OsobineID,
                        principalTable: "Osobina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ptica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OsobinaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ptica", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ptica_Osobina_OsobinaID",
                        column: x => x.OsobinaID,
                        principalTable: "Osobina",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Vidjena",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PticaID = table.Column<int>(type: "int", nullable: false),
                    BrojVidjenja = table.Column<int>(type: "int", nullable: false),
                    PodrucjeID = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vidjena", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vidjena_Podrucje_PodrucjeID",
                        column: x => x.PodrucjeID,
                        principalTable: "Podrucje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vidjena_Ptica_PticaID",
                        column: x => x.PticaID,
                        principalTable: "Ptica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NepoznataPticaOsobina_OsobineID",
                table: "NepoznataPticaOsobina",
                column: "OsobineID");

            migrationBuilder.CreateIndex(
                name: "IX_Ptica_OsobinaID",
                table: "Ptica",
                column: "OsobinaID");

            migrationBuilder.CreateIndex(
                name: "IX_Vidjena_PodrucjeID",
                table: "Vidjena",
                column: "PodrucjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vidjena_PticaID",
                table: "Vidjena",
                column: "PticaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NepoznataPticaOsobina");

            migrationBuilder.DropTable(
                name: "Vidjena");

            migrationBuilder.DropTable(
                name: "NepoznataPtica");

            migrationBuilder.DropTable(
                name: "Podrucje");

            migrationBuilder.DropTable(
                name: "Ptica");

            migrationBuilder.DropTable(
                name: "Osobina");
        }
    }
}
