using System;
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
                name: "Boje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Marke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marke", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Modeli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    DatumProdaje = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modeli", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelID = table.Column<int>(type: "int", nullable: true),
                    MarkaID = table.Column<int>(type: "int", nullable: true),
                    BojaID = table.Column<int>(type: "int", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automobili_Boje_BojaID",
                        column: x => x.BojaID,
                        principalTable: "Boje",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Automobili_Marke_MarkaID",
                        column: x => x.MarkaID,
                        principalTable: "Marke",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Automobili_Modeli_ModelID",
                        column: x => x.ModelID,
                        principalTable: "Modeli",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Automobili_Prodavnice_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_BojaID",
                table: "Automobili",
                column: "BojaID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_MarkaID",
                table: "Automobili",
                column: "MarkaID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_ModelID",
                table: "Automobili",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_ProdavnicaID",
                table: "Automobili",
                column: "ProdavnicaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Boje");

            migrationBuilder.DropTable(
                name: "Marke");

            migrationBuilder.DropTable(
                name: "Modeli");

            migrationBuilder.DropTable(
                name: "Prodavnice");
        }
    }
}
