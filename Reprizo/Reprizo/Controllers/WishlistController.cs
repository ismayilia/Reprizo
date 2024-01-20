using Microsoft.AspNetCore.Mvc;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ISettingService _settingService;

        public WishlistController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
            Dictionary<string, string> wishlistBanner = _settingService.GetSettings();
            ViewBag.WishlistBanner = wishlistBanner["WishlistBanner"];



            return View();
        }
    }
}
