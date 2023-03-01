using Microsoft.AspNetCore.Mvc;
using SistemasVendasDeAutomoveis.Filters;

namespace SistemasVendasDeAutomoveis.Controllers
{
    [PaginaAdmin]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
