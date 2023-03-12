using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemasVendasDeAutomoveis.Migrations
{
    public partial class RecriandoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
         name: "Usuarios",
         columns: table => new
         {
             Id = table.Column<int>(nullable: false)
              .Annotation("SqlServer:Identity", "1, 1"),
             Nome = table.Column<string>(nullable: false),
             Sobrenome = table.Column<string>(nullable: false),
             Email = table.Column<string>(nullable: false),
             Senha = table.Column<string>(nullable: false),
             Celular = table.Column<string>(nullable: false),
             CPF = table.Column<string>(nullable: true),
             Estado = table.Column<string>(nullable: true),
             Cidade = table.Column<string>(nullable: true),
             Endereco = table.Column<string>(nullable: true),
             Perfil = table.Column<int>(nullable: false),
             IsAnunciante = table.Column<bool>(nullable: false),
             DataCadastro = table.Column<DateTime>(nullable: false),
             DataAtualizacao = table.Column<DateTime>(nullable: true),
             CodigoRedefinirSenha = table.Column<string>(nullable: true)
         },
         constraints: table =>
         {
             table.PrimaryKey("PK_Usuarios", x => x.Id);
         });

            migrationBuilder.CreateTable(
               name: "Carros",
               columns: table => new
               {                 
                   Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                   Marca = table.Column<string>(maxLength: 15, nullable: false),
                   Modelo = table.Column<string>(maxLength: 20, nullable: false),
                   Ano = table.Column<int>(nullable: false),
                   Descricao = table.Column<string>(nullable: true, defaultValue: " "),
                   Cor = table.Column<string>(maxLength: 20, nullable: false),
                   Estado = table.Column<int>(nullable: false),
                   Combustivel = table.Column<int>(nullable: false),
                   Cambio = table.Column<int>(nullable: false),
                   Carroceria = table.Column<int>(nullable: false),
                   Quilometragem = table.Column<int>(maxLength: 11, nullable: false),
                   Preco = table.Column<decimal>(maxLength: 14, nullable: false),
                   Vendido = table.Column<bool>(nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Carros", x => x.Id);                
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
