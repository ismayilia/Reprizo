using Microsoft.AspNetCore.Mvc;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class CartController : Controller
    {
        private readonly ISettingService _settingService;

        public CartController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public IActionResult Index()
        {
			Dictionary<string, string> cartBanner = _settingService.GetSettings();
			ViewBag.CartBanner = cartBanner["CartBanner"];




			return View();
        }
    }
}
