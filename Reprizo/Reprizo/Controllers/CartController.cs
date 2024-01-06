using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
