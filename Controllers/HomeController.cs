using Microsoft.AspNetCore.Mvc;

namespace A1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
