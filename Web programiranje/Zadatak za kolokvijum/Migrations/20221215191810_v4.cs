using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "KatProdKuca",
                newName: "KategorijaProdukcijskaKuca");

            migrationBuilder.RenameColumn(
                name: "ProdukcijskaKuca",
                table: "KategorijaProdukcijskaKuca",
                newName: "ListaKucaID");

            migrationBuilder.RenameColumn(
                name: "Kategorija",
                table: "KategorijaProdukcijskaKuca",
                newName: "ListaKategorijaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KategorijaProdukcijskaKuca",
                table: "KategorijaProdukcijskaKuca",
                columns: new[] { "ListaKategorijaID", "ListaKucaID" });

            migrationBuilder.CreateIndex(
                name: "IX_KategorijaProdukcijskaKuca_ListaKucaID",
                table: "KategorijaProdukcijskaKuca",
                column: "ListaKucaID");

            migrationBuilder.AddForeignKey(
                name: "FK_KategorijaProdukcijskaKuca_Kategorije_ListaKategorijaID",
                table: "KategorijaProdukcijskaKuca",
                column: "ListaKategorijaID",
                principalTable: "Kategorije",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KategorijaProdukcijskaKuca_ProdukcijskeKuce_ListaKucaID",
                table: "KategorijaProdukcijskaKuca",
                column: "ListaKucaID",
                principalTable: "ProdukcijskeKuce",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KategorijaProdukcijskaKuca_Kategorije_ListaKategorijaID",
                table: "KategorijaProdukcijskaKuca");

            migrationBuilder.DropForeignKey(
                name: "FK_KategorijaProdukcijskaKuca_ProdukcijskeKuce_ListaKucaID",
                table: "KategorijaProdukcijskaKuca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KategorijaProdukcijskaKuca",
                table: "KategorijaProdukcijskaKuca");

            migrationBuilder.DropIndex(
                name: "IX_KategorijaProdukcijskaKuca_ListaKucaID",
                table: "KategorijaProdukcijskaKuca");

            migrationBuilder.RenameTable(
                name: "KategorijaProdukcijskaKuca",
                newName: "KatProdKuca");

            migrationBuilder.RenameColumn(
                name: "ListaKucaID",
                table: "KatProdKuca",
                newName: "ProdukcijskaKuca");

            migrationBuilder.RenameColumn(
                name: "ListaKategorijaID",
                table: "KatProdKuca",
                newName: "Kategorija");
        }
    }
}
