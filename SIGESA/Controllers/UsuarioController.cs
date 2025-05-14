using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
