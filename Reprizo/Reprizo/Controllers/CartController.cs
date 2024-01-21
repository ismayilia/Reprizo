using Microsoft.AspNetCore.Mvc;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class CartController : Controller
    {
        private readonly ISettingService _settingService;
		private readonly IBasketService _basketService;
		public CartController(ISettingService settingService, IBasketService basketService)
        {
            _settingService = settingService;
			_basketService = basketService;

		}
        public async Task<IActionResult> Index()
        {
			Dictionary<string, string> cartBanner = _settingService.GetSettings();
			ViewBag.CartBanner = cartBanner["CartBanner"];




			return View(await _basketService.GetBasketDatasAsync());
        }


		//[HttpPost]
		//public async Task<IActionResult> Delete(int id)
		//{
		//	var data = await _basketService.DeleteItem(id);

		//	return Ok(data);
		//}

		[HttpPost]
		public async Task<IActionResult> PlusIcon(int id)
		{
			var data = await _basketService.PlusIcon(id);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> MinusIcon(int id)
		{
			var data = await _basketService.MinusIcon(id);
			return Ok(data);
		}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _basketService.DeleteItem(id);

            return Ok(data);
        }
    }
}
