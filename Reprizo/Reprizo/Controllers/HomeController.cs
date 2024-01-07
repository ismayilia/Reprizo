using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Home;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IProductService _productService;
        private readonly ICollectionService _collectionService;



        public HomeController(AppDbContext context, 
                                ISliderService sliderService, 
                                IProductService productService,
                                ICollectionService collectionService)
        {
            _context = context;
            _sliderService = sliderService;
            _productService = productService;
            _collectionService = collectionService;

        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<ProductVM> products = await _productService.GetByTakeWithIncludes(3);
            CollectionVM collection = await _collectionService.GetDataAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Products = products,
                Collection = collection

            };

            return View(model);
        }
    }
}
