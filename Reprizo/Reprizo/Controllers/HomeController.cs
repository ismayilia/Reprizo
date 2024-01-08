using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Areas.Admin.ViewModels.Feature;
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
        private readonly IEssenceService _essenceService;
        private readonly IFeatureService _featureService;




        public HomeController(AppDbContext context, 
                                ISliderService sliderService, 
                                IProductService productService,
                                ICollectionService collectionService,
                                IEssenceService essenceService,
                                IFeatureService featureService)
        {
            _context = context;
            _sliderService = sliderService;
            _productService = productService;
            _collectionService = collectionService;
            _essenceService = essenceService;
            _featureService = featureService;

        }
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _sliderService.GetAllAsync();
            List<ProductVM> products = await _productService.GetAllAsync();
            CollectionVM collection = await _collectionService.GetDataAsync();
            List<EssenceVM> essences = await _essenceService.GetAllAsync();
            FeatureVM feature = await _featureService.GetDataAsync();



            HomeVM model = new()
            {
                Sliders = sliders,
                Products = products,
                Collection = collection,
                Essences = essences,
                Feature = feature

            };

            return View(model);
        }
    }
}
