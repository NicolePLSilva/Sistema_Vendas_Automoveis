using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Filters;
using SistemasVendasDeAutomoveis.Models;
using SistemasVendasDeAutomoveis.Repositorios;

namespace SistemasVendasDeAutomoveis.Controllers
{
    [PaginaAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.ListarTodos();
            return View(usuarios);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Perfil(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                bool excluido = _usuarioRepositorio.Remover(id);
                if(excluido)
                {
                    TempData["MensagemSucesso"] = $"Usuário excluído com êxito!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = $"A exclusão do usuário falhou! Por favor, tente novamente!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não conseguimos excluir o usuário! Por favor, tente novamente! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuarioModel = _usuarioRepositorio.Adicionar(usuarioModel);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Perfil", usuarioModel);
                }
                TempData["MensagemErro"] = $"O cadastro do usuário falhou!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar cadastrar o usuário! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            } 
        }

        [HttpPost]
        public IActionResult Editar(UsuarioModel usuarioModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
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
                        Endereco = usuarioModel.Endereco,
                        Perfil = usuarioModel.Perfil,
                        IsAnunciante = usuarioModel.IsAnunciante
                    };
                    usuario = _usuarioRepositorio.Alterar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Perfil", usuario);
                }
                TempData["MensagemErro"] = $"A alteração do usuário falhou!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Falha ao tentar alterar o usuário! Detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
