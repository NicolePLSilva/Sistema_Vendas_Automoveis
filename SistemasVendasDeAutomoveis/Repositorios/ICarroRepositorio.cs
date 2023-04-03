using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Repositorios
{
    public interface ICarroRepositorio
    {

        List<CarroModel> ListarTodos();

        CarroModel BuscarPorId(int id);

        List<CarroModel> BuscarPorIdVendedor(int usuarioId);

        List<CarroModel> BuscarPorIdComprador(int usuarioId);

        CarroModel Adicionar(CarroModel carro);
        
        bool Remover(int id);

        CarroModel Alterar(CarroModel carro);

        bool Compra(CarroModel carro);
    }
}
