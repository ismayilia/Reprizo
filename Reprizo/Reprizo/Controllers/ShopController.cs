using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers;
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
			Dictionary<string, string> shopBanner = _settingService.GetSettings();

			ViewBag.ShopBanner = shopBanner["ShopBanner"];


			int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

			ShopVM model = new()
            {
                Categories = categories,
                Paginate = paginatedDatas
            };       

            return View(model);
        }

        

        public async Task<IActionResult> GetProductsByCatgeory(int? id, int page = 1, int take = 5)
        {
            if (id is null)
            {
                return BadRequest();
            }

            CategoryVM existCategory = await _categoryService.GetByIdAsync((int)id);

            if (existCategory == null)
            {
                return NotFound();
            }

            List<ProductVM> dbPaginatedDatasByCategory = await _productService.GetPaginatedDatasByCategory((int)id,page, take);
			List<CategoryVM> categories = await _categoryService.GetAllAsync();
			Dictionary<string, string> categoryBanner = _settingService.GetSettings();

			ViewBag.CategoryBanner = categoryBanner["CategoryBanner"];

			int pageCount = await GetPageCountAsync(take);

			Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

			ShopVM model = new()
			{
               CategoryId=(int)id,
				Categories = categories,
				Paginate = paginatedDatas
			};

			return View(model);
		}

        
        public async  Task<IActionResult> ProductDetail(int? id)
        {

			if (id is null)
			{
				return BadRequest();
			}

			ProductVM existProduct = await _productService.GetByIdWithIncludesAsync((int)id);

			if (existProduct == null)
			{
				return NotFound();
			}
			Dictionary<string, string> detailBanner = _settingService.GetSettings();
            ProductVM product = await _productService.GetByIdWithIncludesAsync((int)id);

            ViewBag.ProductDetailBanner = detailBanner["ProductDetailBanner"];

            return View(product);
        }

		private async Task<int> GetPageCountAsync(int take)
		{
			int productCount = await _productService.GetCountAsync();

			return (int)Math.Ceiling((decimal)(productCount) / take);
		}
	}
}
