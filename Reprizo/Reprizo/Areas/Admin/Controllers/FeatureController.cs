using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class FeatureController : MainController
    {
        private readonly IFeatureService _featureService;
		private readonly IMapper _mapper;

		public FeatureController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
			_mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _featureService.GetDataAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			FeatureVM feature = await _featureService.GetByIdAsync((int)id);

			if (feature is null) return RedirectToAction("Index", "Error");

			return View(feature);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			FeatureVM feature = await _featureService.GetByIdAsync((int)id);

			if (feature is null) return RedirectToAction("Index", "Error");

			FeatureEditVM featureEditVM = _mapper.Map<FeatureEditVM>(feature);

			return View(featureEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, FeatureEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            FeatureVM dbFeature = await _featureService.GetByIdAsync((int)id);

            if (dbFeature is null) return RedirectToAction("Index", "Error");


            request.Image = dbFeature.Image;

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


            await _featureService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
