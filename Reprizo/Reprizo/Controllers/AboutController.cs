using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.About;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class AboutController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly ISettingService _settingService;
        public AboutController(IRepairService repairService, ISettingService settingService)
        {
            _repairService = repairService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Index()
        {
            RepairVM repair = await _repairService.GetDataAsync();

            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.AboutBanner = settingDatas["AboutBanner"];

            AboutVM model = new()
            {
                Repair = repair
            };
            return View(model);
        }
    }
}
