using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Areas.Admin.Controllers
{
    public class ErrorController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
