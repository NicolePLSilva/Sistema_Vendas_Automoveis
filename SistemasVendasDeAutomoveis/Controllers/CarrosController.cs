using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class CarrosController : Controller
    {
        private readonly ICarroRepositorio _carroRepositorio;

        public CarrosController(ICarroRepositorio carroRepositorio)
        {
            _carroRepositorio = carroRepositorio;
        }

        public IActionResult Index()
        {
            List<CarroModel> carros = _carroRepositorio.ListarTodos();
            return View(carros);
        }

        public IActionResult Detalhes(int id)
        {
            CarroModel carro = _carroRepositorio.BuscarPorId(id);
            return View(carro);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(CarroInputModel carroModel)
        {
            try
            {
                CarroModel carro = null;

                if (ModelState.IsValid)
                {
                    carro = new CarroModel()
                    {
                        Marca = carroModel.Marca,
                        Modelo = carroModel.Modelo,
                        Descricao = carroModel.Descricao,
                        Ano = carroModel.Ano,
                        Cor = carroModel.Cor,
                        Estado = carroModel.Estado,
                        Carroceria = carroModel.Carroceria,
                        Cambio = carroModel.Cambio,
                        Combustivel = carroModel.Combustivel,
                        Quilometragem = carroModel.QuilometragemParaInt(carroModel.Quilometragem),
                        Preco = carroModel.PrecoParaDecimal(carroModel.Preco)
                    };

                    _carroRepositorio.Adicionar(carro);
                    TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(carroModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao cadastrar o veículo! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
