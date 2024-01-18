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
            if (id is null) return BadRequest();

            TeamVM team = await _teamService.GetByIdAsync((int)id);

            if (team is null) return NotFound();

            return View(team);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			TeamVM team = await _teamService.GetByIdAsync((int)id);

			if (team is null) return NotFound();

			TeamEditVM teamEditVM = _mapper.Map<TeamEditVM>(team);

			return View(teamEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, TeamEditVM request)
        {
            if (id is null) return BadRequest();

            TeamVM dbTeam = await _teamService.GetByIdAsync((int)id);

            if (dbTeam is null) return NotFound();


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
                if (!request.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(request);
                }
            }


            await _teamService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
