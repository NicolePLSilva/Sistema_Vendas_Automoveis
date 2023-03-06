using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Helper;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class GerenciarCadastroController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public GerenciarCadastroController(IUsuarioRepositorio usuario, ISessao sessao)
        {
            _usuarioRepositorio = usuario;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            usuario = _usuarioRepositorio.BuscarPorId(usuario.Id);
            return View(usuario);
        }

        public IActionResult Cadastro(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Cadastro(GerenciarcadastroModel usuarioModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioDB = _usuarioRepositorio.BuscarPorId(usuarioModel.Id);

                    if (usuarioDB == null) throw new Exception();

                    usuario = new UsuarioModel
                    {
                        Id = usuarioModel.Id,
                        Nome = usuarioModel.Nome,
                        Sobrenome = usuarioModel.Sobrenome,
                        Email = usuarioModel.Email,
                        Celular = usuarioModel.Celular,
                        CPF = usuarioModel.CPF,
                        Estado = usuarioModel.Estado,
                        Cidade = usuarioModel.Cidade,
                        Senha = usuarioDB.Senha,
                        Perfil = usuarioDB.Perfil,
                        Endereco = usuarioModel.Endereco,
                        IsAnunciante = usuarioModel.IsAnunciante,
                    };
                    //usuario = _usuarioRepositorio.AlterarCadastro(usuario);
                    usuario = _usuarioRepositorio.Alterar(usuario); //Testar
                    TempData["MensagemSucesso"] = "Salvo com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"A alteração falhou!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar alteração! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
