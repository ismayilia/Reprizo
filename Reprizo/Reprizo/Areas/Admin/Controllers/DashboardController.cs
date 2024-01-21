using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Areas.Admin.Controllers
{
	public class DashboardController : MainController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
