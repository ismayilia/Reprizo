using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
