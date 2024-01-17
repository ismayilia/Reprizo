using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
	public class SliderController : MainController
	{

		private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
			_sliderService = sliderService;

		}

		[HttpGet]
        public async Task<IActionResult> Index()
		{
			return View(await _sliderService.GetAllAsync());
		}

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderVM slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(slider);
        }
    }
}
