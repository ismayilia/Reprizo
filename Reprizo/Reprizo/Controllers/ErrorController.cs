using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
