using Microsoft.AspNetCore.Mvc;

namespace SistemasVendasDeAutomoveis.Controllers
{
    public class CarrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
