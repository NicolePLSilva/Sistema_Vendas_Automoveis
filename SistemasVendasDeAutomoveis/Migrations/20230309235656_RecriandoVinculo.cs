using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasVendasDeAutomoveis.Migrations
{
    public partial class RecriandoVinculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnunciateId",
                table: "Carros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompradorId",
                table: "Carros",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_AnuncianteId",
                table: "Carros",
                column: "AnunciateId");

            migrationBuilder.CreateIndex(
                name: "IX_Carros_CompradorId",
                table: "Carros",
                column: "CompradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Usuarios_AnunciateId",
                table: "Carros",
                column: "AnunciateId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Usuarios_CompradorId",
                table: "Carros",
                column: "CompradorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Usuarios_AnunciateId",
                table: "Carros");

            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Usuarios_CompradorId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_AnuncianteId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_CompradorId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "AnunciateId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "CompradorId",
                table: "Carros");

        }
    }
}
