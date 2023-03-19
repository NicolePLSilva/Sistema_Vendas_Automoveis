using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Helper
{
    public interface IServices
    {
        Task Login(UsuarioModel usuario);

        Task Desconectar();

    }
}
