using Newtonsoft.Json;
using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuarioModel)
        {
            string valor = JsonConvert.SerializeObject(usuarioModel);
            _httpContextAccessor.HttpContext.Session.SetString("sessaoUsuario", valor);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sessaoUsuario");
        }

        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuario = _httpContextAccessor.HttpContext.Session.GetString("sessaoUsuario");

            if (sessaoUsuario == null) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }
    }
}
