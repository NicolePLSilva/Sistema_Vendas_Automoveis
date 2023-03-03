using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuario, ISessao sessao)
        {
            _usuarioRepositorio = usuario;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alterar(AlterarSenhaModel senhaModel)
        {
            try
            {
                UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                senhaModel.Id = usuarioLogado.Id;
                if (ModelState.IsValid)
                {
                    _usuarioRepositorio.AlterarSenha(senhaModel);
                    TempData["MensagemSucesso"] = "Senha alterada com sucesso!";
                    return View("Index", senhaModel);
                }
                return View("Index", senhaModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar alterar a senha! Tente novamente! Detalhe do erro: {erro.Message}";
                return View("Index", senhaModel);
            }
        }
    }
}
