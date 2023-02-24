using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioModel usuarioModel);

        void RemoverSessaoDoUsuario();

        UsuarioModel BuscarSessao();
    }
}
