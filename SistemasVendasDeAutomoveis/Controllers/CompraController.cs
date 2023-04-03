using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class CompraController : Controller
    {
        private readonly ISessao _sessao;
        private readonly ICarroRepositorio _carroRepositorio;

        public CompraController(ISessao sessao, ICarroRepositorio carroRepositorio)
        {
            _sessao = sessao;      
            _carroRepositorio = carroRepositorio;
        }

        public IActionResult Index()
        {        
            return View();
        }

        //[HttpPost]
        public IActionResult EfetuarCompra(int id)
        {
            //código provisório
            CarroModel veiculo = _carroRepositorio.BuscarPorId(id);
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();

            try
            {
                if (veiculo != null && !veiculo.Vendido)
                {
                    veiculo.CompradorId = usuario.Id;
                    bool vendido = _carroRepositorio.Compra(veiculo);

                    if (vendido)
                    {
                        TempData["MensagemSucesso"] = "Veículo comprado com sucesso!";
                        return RedirectToAction("Detalhes", "Carros", new {Id = veiculo.Id});
                    }
                    TempData["MensagemErro"] = "Venda não efetuada!";
                    return View("Index", "Carros");
                }
                TempData["MensagemErro"] = "O veículo já foi vendido ou não está mais em nosso sistema!";
                return View("Index", "Carros");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível efetuar a compra do veículo! Detalhes do erro: {erro.Message}";
                return View("Index", "Carros");
            }
            
        }
    }
}
