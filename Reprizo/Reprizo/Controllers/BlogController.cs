using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
