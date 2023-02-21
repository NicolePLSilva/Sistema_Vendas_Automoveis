using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Repositorios
{
    public interface ICarroRepositorio
    {

        List<CarroModel> ListarTodos();

        CarroModel BuscarPorId(int id);

        CarroModel Adicionar(CarroModel carro);
        
        bool Remover(int id);

        CarroModel Alterar(CarroModel carro);
    }
}
