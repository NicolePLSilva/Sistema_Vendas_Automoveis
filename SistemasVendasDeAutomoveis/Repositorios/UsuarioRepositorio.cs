using SistemasVendasDeAutomoveis.Data;
using SistemasVendasDeAutomoveis.Models;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace SistemasVendasDeAutomoveis.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _context;

        public UsuarioRepositorio(BancoContext context)
        {
            _context = context;
        }

        public List<UsuarioModel> ListarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public UsuarioModel BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public UsuarioModel BuscarPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email.Trim() == email.Trim());
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public bool Remover(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null) throw new Exception("Id não encontrado!");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }

        public UsuarioModel Alterar(UsuarioModel usuarioModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuarioModel.Id);

            if (usuarioDB == null) throw new Exception("Id não encontrado!");

            usuarioDB.Nome = usuarioModel.Nome;
            usuarioDB.Sobrenome = usuarioModel.Sobrenome;
            usuarioDB.Email = usuarioModel.Email;
            usuarioDB.Celular = usuarioModel.Celular;
            usuarioDB.CPF = usuarioModel.CPF;
            usuarioDB.Estado = usuarioModel.Estado;
            usuarioDB.Cidade = usuarioModel.Cidade;
            usuarioDB.Endereco = usuarioModel.Endereco;
            usuarioDB.Perfil = usuarioModel.Perfil;
            usuarioDB.IsAnunciante = usuarioModel.IsAnunciante;
            usuarioDB.Senha = usuarioModel.Senha;
            usuarioDB.DataAtualizacao = DateTime.Now;

            usuarioDB.SetSenhaHash();

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }
    }
}
