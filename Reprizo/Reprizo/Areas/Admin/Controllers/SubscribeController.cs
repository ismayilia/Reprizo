using Microsoft.AspNetCore.Mvc;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
	public class SubscribeController : MainController
	{
		private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
			_subscribeService = subscribeService;

		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _subscribeService.GetAllAsync());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _subscribeService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
