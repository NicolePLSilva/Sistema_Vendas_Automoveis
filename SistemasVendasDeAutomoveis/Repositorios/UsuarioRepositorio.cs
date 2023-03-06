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
            return _context.Usuarios.Single(u => u.Id == id);
        }

        public UsuarioModel BuscarPorEmail(string email)
        {
            return _context.Usuarios.Single(u => u.Email.Trim() == email.Trim());
        }


        public UsuarioModel BuscarPorCodigoRedefinicao(string codigo)
        {
            return _context.Usuarios.Single(u => u.CodigoRedefinirSenha == codigo);
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
            usuarioDB.CodigoRedefinirSenha = usuarioModel.CodigoRedefinirSenha;

            usuarioDB.SetSenhaHash();

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Não foi possível alterar a senha pois o usuário não foi encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não é válida!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("A nova senha deve ser diferente da senha atual!");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;


        }

    }
}
