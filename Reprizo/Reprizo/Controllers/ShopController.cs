using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;

        public ShopController(IProductService productService, ISettingService settingService)
        {
            _productService = productService;
            _settingService = settingService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> ProductDetail(int id)
        {
            Dictionary<string, string> detailBanner = _settingService.GetSettings();
            ProductVM product = await _productService.GetByIdWithIncludesAsync(id);

            ViewBag.ProductDetailBanner = detailBanner["ProductDetailBanner"];

            return View(product);
        }
    }
}
