﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Data;
using Reprizo.Helpers;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
	public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly IBasketService _basketService;
        private readonly IWishlistService _wishlistService;


        public ShopController(IProductService productService, 
                                                            ISettingService settingService,
                                                            ICategoryService categoryService,
															IBasketService basketService,
															IWishlistService wishlistService)
        {
            _productService = productService;
            _settingService = settingService;
            _categoryService = categoryService;
			_basketService = basketService;
			_wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 6)
        {
            List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);
            List<CategoryVM> categories = await _categoryService.GetAllAsync();
			Dictionary<string, string> shopBanner = _settingService.GetSettings();
            int count = await _productService.GetCountAsync();

			ViewBag.ShopBanner = shopBanner["ShopBanner"];


			int pageCount = await GetPageCountAsync(take);

            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

			ShopVM model = new()
            {
                Categories = categories,
                Paginate = paginatedDatas,
                Count = count
            };       

            return View(model);
        }

        

        public async Task<IActionResult> GetProductsByCatgeory(int? id, int page = 1, int take = 6)
        {
            if (id is null)
            {
                return RedirectToAction("Index", "Error"); ;
            }

            CategoryVM existCategory = await _categoryService.GetByIdAsync((int)id);

            if (existCategory == null)
            {
                return RedirectToAction("Index", "Error"); ;
            }

            var count = await _productService.GetCountByCategoryAsync((int)id);

            List<ProductVM> dbPaginatedDatasByCategory = await _productService.GetPaginatedDatasByCategory((int)id,page, take);
			List<CategoryVM> categories = await _categoryService.GetAllAsync();
			Dictionary<string, string> categoryBanner = _settingService.GetSettings();

			ViewBag.CategoryBanner = categoryBanner["CategoryBanner"];

			int pageCount = await GetPageCountByCategoryAsync((int)id,take);

			Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasByCategory, page, pageCount);

			ShopVM model = new()
			{
               CategoryId=(int)id,
				Categories = categories,
				Paginate = paginatedDatas,
                Count = count
			};

			return View(model);
		}

        
        public async  Task<IActionResult> ProductDetail(int? id)
        {

			if (id is null)
			{
				return RedirectToAction("Index", "Error"); ;
			}

			ProductVM existProduct = await _productService.GetByIdWithIncludesAsync((int)id);

			if (existProduct == null)
			{
				return RedirectToAction("Index", "Error"); ;
			}
			Dictionary<string, string> detailBanner = _settingService.GetSettings();
            ProductVM product = await _productService.GetByIdWithIncludesAsync((int)id);

            ViewBag.ProductDetailBanner = detailBanner["ProductDetailBanner"];

            return View(product);
        }



		
        public async Task<IActionResult> Search(string searchText, int page = 1, int take = 6)
        {

            if (searchText == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            List<ProductVM> dbPaginatedDatasBySearch = await _productService.SearchAsync(searchText, page, take);
            
            List<CategoryVM> categories = await _categoryService.GetAllAsync();

            int count = await _productService.GetCountBySearch(searchText);
            
            int pageCount = await GetPageCountBySearchAsync(searchText,take);
            
            Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatasBySearch, page, pageCount);
			
            Dictionary<string, string> shopBanner = _settingService.GetSettings();
            
            ViewBag.ShopBanner = shopBanner["ShopBanner"];

			ShopVM model = new()
            {
                Categories = categories,
                Paginate = paginatedDatas,
                SearchText=searchText,
                Count = count

            };

            return View(model);
        }


		public async Task<IActionResult> Sort(string sortValue, int page = 1, int take = 6)
		{
			List<ProductVM> products = new();

			if (sortValue == "1")
			{
				return RedirectToAction(nameof(Index));
			};
			if (sortValue == "2")
			{
				products = await _productService.OrderByNameAsc(page, take);

			};
			if (sortValue == "3")
			{
				products = await _productService.OrderByNameDesc(page, take);

			};
			if (sortValue == "4")
			{
				products = await _productService.OrderByPriceAsc(page, take);

			};
			
			if (sortValue == "5")
			{
				products = await _productService.OrderByPriceDesc(page, take);

			};


			int pageCount = await GetPageCountAsync(take);

			Paginate<ProductVM> paginatedDatas = new(products, page, pageCount);

			List<CategoryVM> categories = await _categoryService.GetAllAsync();

			int count = await _productService.GetCountAsync();
            Dictionary<string, string> shopBanner = _settingService.GetSettings();

            ViewBag.ShopBanner = shopBanner["ShopBanner"];

            ShopVM model = new()
			{
				Paginate = paginatedDatas,
				Categories = categories,
				Count = count,
				SortValue = sortValue
			};
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Filter(int value1, int value2)
		{

			List<ProductVM> productsByFilter = await _productService.FilterAsync(value1, value2);
			return PartialView("_ProductsPartial", productsByFilter);

		}

		public async Task<IActionResult> FilterCount(int value1, int value2)
		{

			int filterCount = await _productService.FilterCountAsync(value1, value2);
			return Ok(filterCount);

		}


		private async Task<int> GetPageCountAsync(int take)
		{
			int productCount = await _productService.GetCountAsync();

			return (int)Math.Ceiling((decimal)(productCount) / take);
		}

		private async Task<int> GetPageCountByCategoryAsync(int id,int take)
		{
			int productCount = await _productService.GetCountByCategoryAsync(id);

			return (int)Math.Ceiling((decimal)(productCount) / take);
		}

		private async Task<int> GetPageCountBySearchAsync(string searchText, int take)
		{
			int productCount = await _productService.GetCountBySearch(searchText);

			return (int)Math.Ceiling((decimal)(productCount) / take);
		}



		[HttpPost]

		public async Task<IActionResult> AddBasket(int? id)
		{


			if (id is null) return RedirectToAction("Index", "Error"); ;

			ProductVM product = await _productService.GetByIdWithIncludesAsync((int)id);

			if (product is null) return RedirectToAction("Index", "Error"); ;

			_basketService.AddBasket((int)id, product);


			return Ok();
		}

		[HttpPost]

		public async Task<IActionResult> AddWishlist(int? id)
		{


			if (id is null) return RedirectToAction("Index", "Error"); ;

			ProductVM product = await _productService.GetByIdWithIncludesAsync((int)id);

			if (product is null) return RedirectToAction("Index", "Error"); ;

			int a =_wishlistService.AddWishlist((int)id, product);

			return Ok(a);
		}


	}
}
