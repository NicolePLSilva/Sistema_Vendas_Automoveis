using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasVendasDeAutomoveis.Migrations
{
    public partial class AjusteVinculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "VendedorId",
                table: "Carros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
              name: "CompradorId",
              table: "Carros",
              type: "int",
              nullable: true,
              defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_CompradorId",
                table: "Carros",
                column: "CompradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_VendedorId",
                table: "Carros",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Usuarios_CompradorId",
                table: "Carros",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Usuarios_VendedorId",
                table: "Carros",
                column: "VendedorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict,
                onUpdate: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Usuarios_CompradorId",
                table: "Carros");

            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Usuarios_VendedorId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_CompradorId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_VendedorId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "CompradorId",
                table: "Carros");       
        }
    }
}
