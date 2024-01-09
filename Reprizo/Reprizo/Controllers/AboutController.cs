using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.About;
using Reprizo.Areas.Admin.ViewModels.BestWorker;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Areas.Admin.ViewModels.Team;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class AboutController : Controller
    {
        private readonly IRepairService _repairService;
        private readonly ISettingService _settingService;
        private readonly ITeamService _teamService;
        private readonly IBestWorkerService _bestWorkerService;

        public AboutController(IRepairService repairService, 
                                                            ISettingService settingService,
                                                            ITeamService teamService,
                                                            IBestWorkerService bestWorkerService)
        {
            _repairService = repairService;
            _settingService = settingService;
            _teamService = teamService;
            _bestWorkerService = bestWorkerService;
        }
        public async Task<IActionResult> Index()
        {
            RepairVM repair = await _repairService.GetDataAsync();

            List<TeamVM> teams = await _teamService.GetAllAsync();

            BestWorkerVM best = await _bestWorkerService.GetDataAsync();

            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.AboutBanner = settingDatas["AboutBanner"];

            AboutVM model = new()
            {
                Repair = repair,
                Teams = teams,
                Best = best
            };
            return View(model);
        }
    }
}
