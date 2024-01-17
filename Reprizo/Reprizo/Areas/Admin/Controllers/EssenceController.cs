using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class EssenceController : MainController
    {
        private readonly IEssenceService _essenceService;
		private readonly IMapper _mapper;
		public EssenceController(IEssenceService essenceService, IMapper mapper)
        {
            _essenceService = essenceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _essenceService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            EssenceVM essence = await _essenceService.GetByIdAsync((int)id);

            if (essence is null) return NotFound();

            return View(essence);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			EssenceVM essence = await _essenceService.GetByIdAsync((int)id);

			if (essence is null) return NotFound();

			EssenceEditVM essenceEditVM = _mapper.Map<EssenceEditVM>(essence);

			return View(essenceEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EssenceEditVM request)
        {
            if (id is null) return BadRequest();

            EssenceVM dbEssence = await _essenceService.GetByIdAsync((int)id);

            if (dbEssence is null) return NotFound();


            request.Image = dbEssence.Image;

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


            await _essenceService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
