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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarroModel>()
                .HasOne(x => x.Vendedor)
                .WithMany(x => x.VeiculosAnunciados)
                .HasForeignKey(x => x.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CarroModel>()
                .HasOne(x => x.Comprador)
                .WithMany(x => x.VeiculosComprados)
                .HasForeignKey(x => x.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
