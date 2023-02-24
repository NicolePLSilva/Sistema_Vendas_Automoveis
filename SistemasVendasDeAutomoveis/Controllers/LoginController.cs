using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Models;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entrar(LoginModel loginModel)
        {

            return View();
        }
    }
}
