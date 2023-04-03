using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Filters;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace SistemasVendasDeAutomoveis.Controllers
{
    [PaginaUser]
    public class CarrosController : Controller
    {
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly ISessao _sessao;

        public CarrosController(ICarroRepositorio carroRepositorio,
            ISessao sessao)
        {
            _carroRepositorio = carroRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index(string marca, string modelo, int anoMin, int anoMax, int kmMax, string carroceria,
            string combustivel, string cambio, string cor, decimal precoMin, decimal precoMax)
        {
            List<CarroModel> carros = _carroRepositorio.ListarTodos();

            carros = carros.Where(c => c.Vendido == false).ToList();

            carros = carros.Where(c =>
                (marca == null || c.Marca.ToLower() == marca.ToLower()) &&
                (modelo == null || c.Modelo.ToLower() == modelo.ToLower()) &&
                (anoMin == 0 || c.Ano >= anoMin) &&
                (anoMax == 0 || c.Ano <= anoMax) &&
                (kmMax == 0 || c.Quilometragem <= kmMax) &&
                (carroceria == null || c.Carroceria.ToString() == carroceria) &&
                (combustivel == null || c.Combustivel.ToString() == combustivel) &&
                (cambio == null || c.Cambio.ToString() == cambio) &&
                (cor == null || c.Cor.ToLower() == cor.ToLower()) &&
                (precoMin == 0 || c.Preco >= precoMin) &&
                (precoMax == 0 || c.Preco <= precoMax)
            ).ToList();


            ViewBag.MarcaSelecionada = marca;
            ViewBag.ModeloSelecionado = modelo;
            ViewBag.AnoMin = anoMin;
            ViewBag.AnoMax = anoMax;
            ViewBag.KmMax = kmMax;
            ViewBag.PrecoMin = precoMin;
            ViewBag.PrecoMax = precoMax;
            ViewBag.Combustivel = combustivel;
            ViewBag.Carroceria = carroceria;
            ViewBag.Cambio = cambio;
            ViewBag.Cor = cor;


            var carrosAgrupados = carros.GroupBy(c => c.Marca);
            var modelosAgrupados = carros.GroupBy(c => c.Modelo);
            var coresAgrupadas = carros.GroupBy(c => c.Cor);

            ViewBag.CarrosAgrupados = carrosAgrupados.ToList();
            ViewBag.ModelosAgrupados = modelosAgrupados.ToList();
            ViewBag.CoresAgrupadas = coresAgrupadas.ToList();


            return View(carros.ToList());
        }

        [PaginaUserAnunciante]
        public IActionResult Listagem()
        {
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            if (usuario.Perfil != Enums.PerfilEnum.ADMIN) return RedirectToAction("Index", "Home");
            List<CarroModel> carros = _carroRepositorio.ListarTodos();
            return View(carros);
        }

        [PaginaUser]
        public IActionResult Detalhes(int id)
        {
            CarroModel carro = _carroRepositorio.BuscarPorId(id);
            return View(carro);
        }

        [PaginaUserAnunciante]
        public IActionResult Gerenciar(int id)
        {
            CarroModel carro = _carroRepositorio.BuscarPorId(id);

            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            if (carro.VendedorId != usuario.Id && usuario.Perfil != Enums.PerfilEnum.ADMIN)
            {
                TempData["MensagemErro"] = "Você não tem persmissão para gerenciar aquele veículo!";
                return RedirectToAction("Index", "Home");
            }

            return View("GerenciarVeiculo", carro);
        }

        [PaginaUserAnunciante]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [PaginaUserAnunciante]
        public IActionResult Editar(int id)
        {
            CarroModel carroModel = _carroRepositorio.BuscarPorId(id);

            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            if (carroModel.VendedorId != usuario.Id && usuario.Perfil != Enums.PerfilEnum.ADMIN)
            {
                TempData["MensagemErro"] = "Você não tem persmissão para gerenciar aquele veículo!";
                return RedirectToAction("Index", "Home");
            }

            return View(carroModel);
        }

        [PaginaUserAnunciante]
        public IActionResult Excluir(int id)
        {
            CarroModel carro = _carroRepositorio.BuscarPorId(id);

            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            if (carro.VendedorId != usuario.Id && usuario.Perfil != Enums.PerfilEnum.ADMIN)
            {
                TempData["MensagemErro"] = "Você não tem persmissão para gerenciar aquele veículo!";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                bool removido = _carroRepositorio.Remover(id);
                if (removido)
                {
                    TempData["MensagemSucesso"] = "Veículo excluído com sucesso!";
                    return RedirectToAction("Listagem");
                }
                TempData["MensagemErro"] = $"A exclusão do veículo falhou!";
                return RedirectToAction("Listagem");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar excluir o veículo! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Listagem");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(CarroInputModel carroModel)
        {
            try
            {
                CarroModel carro = null;
                UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
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
                            Preco = carroModel.PrecoParaDecimal(carroModel.Preco),
                            CompradorId = null,
                            VendedorId = usuario.Id
                        };

                        _carroRepositorio.Adicionar(carro);
                        TempData["MensagemSucesso"] = "Veículo cadastrado com sucesso!";
                        return RedirectToAction("Gerenciar", carro);
                    }

                    TempData["MensagemErro"] = $"Ops, um erro inesperado aconteceu! Não conseguimos cadastrar o veículo!" +
                        $"Por favor, tente novamente.";
                    return RedirectToAction("Listagem");

                }

                return View(carroModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao cadastrar o veículo! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Listagem");
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
                        return RedirectToAction("Listagem");
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
