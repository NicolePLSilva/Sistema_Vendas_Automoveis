using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SistemasVendasDeAutomoveis.Models;
using System.Security.Claims;

namespace SistemasVendasDeAutomoveis.Helper
{
    public class Services: IServices
    {
        private readonly IHttpContextAccessor _httpContext;

        public Services(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public async Task Login(UsuarioModel usuario)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));
            claims.Add(new Claim(ClaimTypes.Role, usuario.Perfil.ToString()));

            var claimsIdentity = new ClaimsPrincipal(
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                ));

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.Now.AddDays(7),
                IssuedUtc = DateTime.Now,
                IsPersistent = true
            };

            await _httpContext.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsIdentity,
                authProperties);
        }

        public async Task Desconectar()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme) ;
        }
    }
}
