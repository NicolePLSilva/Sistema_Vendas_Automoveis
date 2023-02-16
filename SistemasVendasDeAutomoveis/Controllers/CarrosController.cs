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

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(CarroModel carroModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _carroRepositorio.Adicionar(carroModel);
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
