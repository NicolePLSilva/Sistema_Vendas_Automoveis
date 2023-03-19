using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Data;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;
using System.Security.Claims;
using SistemasVendasDeAutomoveis.Filters;
using Microsoft.AspNetCore.Authorization;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        private readonly IServices _services;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao,
                                IEmail email, IServices services)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
            _services = services;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        [PaginaUser]
        public async Task<IActionResult> Sair()
        {
            await _services.Desconectar();
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [AllowAnonymous]
        public IActionResult Cadastrar()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult DefinirNovaSenha(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            try
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorCodigoRedefinicao(id);

                if (usuario == null) return NotFound();

                RedefinirNovaSenhaModel novaSenha = new RedefinirNovaSenhaModel();

                novaSenha.CodigoRedefinicao = id;

                return View(novaSenha);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginModel loginModel)
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
                            if (loginModel.LembrarLogin) await _services.Login(usuario);
                           
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
                TempData["MensagemErro"] = "Falha ao cadastrar! Por favor, tente novamente!";
                return View();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos realizar o cadastro! Por favor, tente novamente! Detalhes do erro {erro.Message}";
                return View();
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkDeRedefinicao(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmail(redefinirSenhaModel.Email);
                    if (usuario != null)
                    {
                        string codigoReset = Guid.NewGuid().ToString();
                        var verificarUrl = "/Login/DefinirNovaSenha/" + codigoReset;
                        var uri = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}");
                        var link = uri.AbsoluteUri.Replace(uri.PathAndQuery, verificarUrl);

                        usuario.CodigoRedefinirSenha = codigoReset;
                        _usuarioRepositorio.Alterar(usuario);

                        var assunto = "Link de Redefinição de Senha";
                        var mensagem = "Olá " + usuario.Nome + ", <br/> Você fez uma solicitação para redefinir sua senha. " +
                            "Clique no link abaixo para realizar a redefinição!" +
                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "Se você não fez essa solicitação ignore este e-mail.<br/><br/> AutoNovos Agradece!";


                        bool emailEnviado = _email.EnviarEmail(usuario.Email, assunto, mensagem);

                        if (emailEnviado)
                        {
                            TempData["MensagemSucesso"] = "O link de redefinição de senha foi enviado para o seu endereço de e-mail.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar o e-mail para redefinição de senha! Por favor, tente novamente!";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = "Não conseguimos enviar o e-mail de redefinição de senha! Verifique os dados informados e tente novamente!";
                }

                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Falha ao redefinir senha! Por favor tente novamente! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult DefinirNovaSenha(RedefinirNovaSenhaModel senhaModel)
        {

            try
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorCodigoRedefinicao(senhaModel.CodigoRedefinicao);
                if (ModelState.IsValid)
                {
                    usuario.Senha = senhaModel.NovaSenha;

                    usuario.CodigoRedefinirSenha = "";

                    _usuarioRepositorio.Alterar(usuario);
                    TempData["MensagemSucesso"] = "Nova Senha Atualizada!";
                    return RedirectToAction("Index", "Login");
                }

                TempData["MensagemErro"] = "Erro ao atualizar senha! Por favor, tente novamente!";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos atualizar sua senha! Por favor, tente novamente! Detalhes do erro {erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
