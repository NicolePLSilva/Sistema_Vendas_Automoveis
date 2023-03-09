using Microsoft.EntityFrameworkCore;
using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<CarroModel> Carros { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        

    }
}
