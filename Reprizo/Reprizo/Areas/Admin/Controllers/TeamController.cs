using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Team;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class TeamController : MainController
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _teamService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            TeamVM team = await _teamService.GetByIdAsync((int)id);

            if (team is null) return RedirectToAction("Index", "Error");

            return View(team);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			TeamVM team = await _teamService.GetByIdAsync((int)id);

			if (team is null) return RedirectToAction("Index", "Error");

			TeamEditVM teamEditVM = _mapper.Map<TeamEditVM>(team);

			return View(teamEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            TeamVM dbTeam = await _teamService.GetByIdAsync((int)id);

            if (dbTeam is null) return RedirectToAction("Index", "Error");


            request.Image = dbTeam.Image;

            if (!ModelState.IsValid)
            {
                return View(request);

            }

            if (request.Photo is not null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }
                if (!request.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "File size can be max 500 kb");
                    return View(request);
                }
            }


            await _teamService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
