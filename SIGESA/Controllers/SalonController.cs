using Microsoft.AspNetCore.Mvc;

namespace SigesaWeb.Controllers
{
    public class SalonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
