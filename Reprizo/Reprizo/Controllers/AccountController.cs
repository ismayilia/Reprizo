using Microsoft.AspNetCore.Mvc;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISettingService _settingService;

        public AccountController(ISettingService settingService)
        {
			_settingService = settingService;

		}

        [HttpGet]
        public IActionResult Register()
        {
			Dictionary<string, string> settingDatas = _settingService.GetSettings();

			ViewBag.RegisterBanner = settingDatas["RegisterBanner"];
			return View();
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
			Dictionary<string, string> settingDatas = _settingService.GetSettings();

			ViewBag.LoginBanner = settingDatas["LoginBanner"];
			return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
			Dictionary<string, string> settingDatas = _settingService.GetSettings();

			ViewBag.ForgotBanner = settingDatas["ForgotBanner"];
			return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
