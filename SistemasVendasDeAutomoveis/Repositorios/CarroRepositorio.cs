using SistemasVendasDeAutomoveis.Data;
using SistemasVendasDeAutomoveis.Models;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

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
            return _context.Carros.FirstOrDefault(c => c.Id == id);
        }

        public CarroModel Adicionar(CarroModel carro)
        {
            _context.Carros.Add(carro);
            _context.SaveChanges();

            return carro;
        }

        public bool Remover(int id)
        {
            CarroModel carroDB = BuscarPorId(id);

            if (carroDB == null) throw new Exception("Id não encontrado!");

            _context.Carros.Remove(carroDB);
            _context.SaveChanges();

            return true;
        }

        public CarroModel Alterar(CarroModel carroModel)
        {
            CarroModel carroDB = BuscarPorId(carroModel.Id);

            if (carroDB == null) throw new Exception("Id não encontrado!");

            carroDB.Marca = carroModel.Marca;
            carroDB.Modelo = carroModel.Modelo;
            carroDB.Descricao = carroModel.Descricao;
            carroDB.Ano = carroModel.Ano;
            carroDB.Cor = carroModel.Cor;
            carroDB.Estado = carroModel.Estado;
            carroDB.Cambio = carroModel.Cambio;
            carroDB.Combustivel = carroModel.Combustivel;
            carroDB.Carroceria = carroModel.Carroceria;
            carroDB.Quilometragem = carroModel.Quilometragem;
            carroDB.Preco = carroModel.Preco;

            _context.Carros.Update(carroDB);
            _context.SaveChanges();

            return carroDB;
        }
    }
}
