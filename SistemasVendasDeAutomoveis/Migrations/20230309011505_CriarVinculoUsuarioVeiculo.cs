using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasVendasDeAutomoveis.Migrations
{
    public partial class CriarVinculoUsuarioVeiculo : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModelId",
                table: "Carros",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Vendido",
                table: "Carros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Carros_UsuarioModelId",
                table: "Carros",
                column: "UsuarioModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carros_Usuarios_UsuarioModelId",
                table: "Carros",
                column: "UsuarioModelId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carros_Usuarios_UsuarioModelId",
                table: "Carros");

            migrationBuilder.DropIndex(
                name: "IX_Carros_UsuarioModelId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "AnunciateId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "CompradorId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "UsuarioModelId",
                table: "Carros");

            migrationBuilder.DropColumn(
                name: "Vendido",
                table: "Carros");
        }
    }
}
