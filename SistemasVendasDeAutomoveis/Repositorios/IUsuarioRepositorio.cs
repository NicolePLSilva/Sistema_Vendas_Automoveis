using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Repositorios
{
    public interface IUsuarioRepositorio
    {

        List<UsuarioModel> ListarTodos();

        UsuarioModel BuscarPorId(int id);

        UsuarioModel BuscarPorEmail(string email);

        UsuarioModel Adicionar(UsuarioModel usuario);
        
        bool Remover(int id);

        UsuarioModel Alterar(UsuarioModel usuario);
    }
}
