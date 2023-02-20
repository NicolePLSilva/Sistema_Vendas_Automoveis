using SistemasVendasDeAutomoveis.Data;
using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Repositorios
{
    public class CarroRepositorio : ICarroRepositorio
    {
        private readonly BancoContext _context;

        public CarroRepositorio(BancoContext context)
        {
            _context = context;
        }

        public List<CarroModel> ListarTodos()
        {
            return _context.Carros.ToList();
        }

        public CarroModel BuscarPorId(int id)
        {
            return _context.Carros.Single(c => c.Id == id);
        }

        public CarroModel Adicionar(CarroModel carro)
        {
            _context.Carros.Add(carro);
            _context.SaveChanges();
            return carro;
        }

        public bool Remover(CarroModel carro)
        {
            _context.Carros.Remove(carro);
            _context.SaveChanges();
            return true;
        }
    }
}
