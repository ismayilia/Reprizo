using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class SliderController : MainController
	{

		private readonly ISliderService _sliderService;
        private readonly IMapper _mapper; 

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
			_sliderService = sliderService;
            _mapper = mapper;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			return View(await _sliderService.GetAllAsync());
		}

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return RedirectToAction("Index", "Error");

            return View(slider);
        }

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(SliderCreateVM request)

        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", "File size can be max 500 kb");
                return View();
            }

            await _sliderService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            SliderEditVM sliderEditVM = _mapper.Map<SliderEditVM>(slider);

            return View(sliderEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            SliderVM dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return RedirectToAction("Index", "Error");


            request.Image = dbSlider.Image;

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


            await _sliderService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
