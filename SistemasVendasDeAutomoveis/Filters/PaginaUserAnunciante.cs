using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Filters
{
    public class PaginaUserAnunciante : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } }); 
            }
            else
            {
                UsuarioModel usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuarioModel == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login" }, {"action","Index" } });
                }

                if (usuarioModel.IsAnunciante != true && usuarioModel.Perfil != Enums.PerfilEnum.ADMIN)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
