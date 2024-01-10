using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;


        public ShopController(IProductService productService, 
                                                            ISettingService settingService,
                                                            ICategoryService categoryService)
        {
            _productService = productService;
            _settingService = settingService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();

            int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            ShopVM model = new()
            {
                Categories = categories,
                Paginate = paginatedDatas
            };       

            return View(model);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)(productCount) / take);
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
