using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class RepairController : MainController
    {
        private readonly IRepairService _repairService;
        private readonly IMapper _mapper;


        public RepairController(IRepairService repairService, IMapper mapper)
        {
            _repairService = repairService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repairService.GetDataAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			RepairVM repair = await _repairService.GetByIdAsync((int)id);

			if (repair is null) return RedirectToAction("Index", "Error");

			return View(repair);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			RepairVM repair = await _repairService.GetByIdAsync((int)id);

			if (repair is null) return RedirectToAction("Index", "Error");

			RepairEditVM repairEditVM = _mapper.Map<RepairEditVM>(repair);

			return View(repairEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, RepairEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            RepairVM dbRepair = await _repairService.GetByIdAsync((int)id);

            if (dbRepair is null) return RedirectToAction("Index", "Error");


            request.Image = dbRepair.Image;

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


            await _repairService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
