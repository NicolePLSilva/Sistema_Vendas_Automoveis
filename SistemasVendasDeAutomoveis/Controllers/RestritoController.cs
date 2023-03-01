using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Filters;

namespace SistemasVendasDeAutomoveis.Controllers
{
    [PaginaUser]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
