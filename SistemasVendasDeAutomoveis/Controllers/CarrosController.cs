using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

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

        public IActionResult Editar(int id)
        {
            CarroModel carroModel = _carroRepositorio.BuscarPorId(id);
            return View(carroModel);
        }


        public IActionResult Excluir(int id)
        {
            try
            {
                bool removido = _carroRepositorio.Remover(id);
                if (removido)
                {
                    TempData["MensagemSucesso"] = "Veículo excluído com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"A exclusão do veículo falhou!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar excluir o veículo! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(CarroInputModel carroModel)
        {
            try
            {
                CarroModel carro = null;

                if (ModelState.IsValid)
                {
                    if (carroModel.Estado != null && carroModel.Cambio != null && carroModel.Combustivel != null &&
                        carroModel.Ano != null && carroModel.Carroceria != null && carroModel.Preco != null)
                    {
                        carro = new CarroModel()
                        {
                            Marca = carroModel.Marca,
                            Modelo = carroModel.Modelo,
                            Descricao = carroModel.Descricao,
                            Ano = carroModel.Ano ?? throw new Exception(),
                            Cor = carroModel.Cor,
                            Estado = carroModel.Estado ?? throw new Exception(),
                            Cambio = carroModel.Cambio ?? throw new Exception(),
                            Combustivel = carroModel.Combustivel ?? throw new Exception(),
                            Carroceria = carroModel.Carroceria ?? throw new Exception(),
                            Quilometragem = carroModel.QuilometragemParaInt(carroModel.Quilometragem),
                            Preco = carroModel.PrecoParaDecimal(carroModel.Preco)
                        };

                        _carroRepositorio.Adicionar(carro);
                        TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso!";
                        return RedirectToAction("Detalhes", carro);
                    }

                    TempData["MensagemErro"] = $"Ops, um erro inesperado aconteceu! Não conseguimos cadastrar o veículo!" +
                        $"Por favor, tente novamente.";
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

        [HttpPost]
        public IActionResult Editar(CarroInputModel carroModel)
        {
            try
            {
                CarroModel carro = null;

                if (ModelState.IsValid)
                {
                    if (carroModel.Estado != null && carroModel.Cambio != null && carroModel.Combustivel != null &&
                        carroModel.Ano != null && carroModel.Carroceria != null && carroModel.Preco != null)
                    {
                        carro = new CarroModel()
                        {
                            Id = carroModel.Id,
                            Marca = carroModel.Marca,
                            Modelo = carroModel.Modelo,
                            Descricao = carroModel.Descricao,
                            Ano = carroModel.Ano ?? throw new Exception(),
                            Cor = carroModel.Cor,
                            Estado = carroModel.Estado ?? throw new Exception(),
                            Cambio = carroModel.Cambio ?? throw new Exception(),
                            Combustivel = carroModel.Combustivel ?? throw new Exception(),
                            Carroceria = carroModel.Carroceria ?? throw new Exception(),
                            Quilometragem = carroModel.QuilometragemParaInt(carroModel.Quilometragem),
                            Preco = carroModel.PrecoParaDecimal(carroModel.Preco)
                        };

                        carro = _carroRepositorio.Alterar(carro);
                        TempData["MensagemSucesso"] = "Veículo atualizado com sucesso!";
                        return RedirectToAction("Detalhes", carro);
                    }

                    TempData["MensagemErro"] = $"Ops, um erro inesperado aconteceu! Não conseguimos atualizar o veículo!" +
                        $"Por favor, tente novamente.";
                    return View(carro);

                }

                return View(carroModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao atualizar o veículo! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
