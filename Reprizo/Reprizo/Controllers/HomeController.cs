using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Home;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public HomeController(AppDbContext context, ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders
            };

            return View(model);
        }
    }
}
