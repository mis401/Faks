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
            migrationBuilder.AddColumn<int>(
                name: "MarkaID",
                table: "Modeli",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modeli_MarkaID",
                table: "Modeli",
                column: "MarkaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modeli_Marke_MarkaID",
                table: "Modeli",
                column: "MarkaID",
                principalTable: "Marke",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modeli_Marke_MarkaID",
                table: "Modeli");

            migrationBuilder.DropIndex(
                name: "IX_Modeli_MarkaID",
                table: "Modeli");

            migrationBuilder.DropColumn(
                name: "MarkaID",
                table: "Modeli");
        }
    }
}
