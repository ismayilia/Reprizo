using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
