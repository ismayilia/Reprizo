using Microsoft.AspNetCore.Mvc;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ISettingService _settingService;
		private readonly IWishlistService _wishlistService;

		public WishlistController(ISettingService settingService, IWishlistService wishlistService)
        {
            _settingService = settingService;
            _wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            Dictionary<string, string> wishlistBanner = _settingService.GetSettings();
            ViewBag.WishlistBanner = wishlistBanner["WishlistBanner"];

			return View(await _wishlistService.GetWishlistDatasAsync());

        }

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var data =  _wishlistService.DeleteItem(id);

			return Ok(data);
		}
	}
}
