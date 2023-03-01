using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Cadastrar()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmail(loginModel.Email);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha inválida!";
                    }
                    TempData["MensagemErro"] = $"E-mail ou Senha inválidos! Por favor, verifique os dados e tente novamente!";
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos iniciar sua sessão! Por favor, tente novamente! Detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastroModel cadastroModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel
                    {
                        Nome = cadastroModel.Nome,
                        Sobrenome = cadastroModel.Sobrenome,
                        Email = cadastroModel.Email,
                        Celular = cadastroModel.Celular,
                        Senha = cadastroModel.Senha,

                        Perfil = Enums.PerfilEnum.USER,
                        IsAnunciante = false,
                    };

                    _usuarioRepositorio.Adicionar(usuario);
                    return RedirectToAction("Index", "Home");
                }
                TempData["MensagemErro"] = $"Falha ao cadastrar! Por favor, tente novamente!";
                return View();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar o cadastro! Por favor, tente novamente! Detalhes do erro {erro.Message}";
                return View();
            }
        }
    }
}
