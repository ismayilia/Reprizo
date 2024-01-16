using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Helpers;
using Reprizo.Helpers.Extensions;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
	public class ProductController : MainController
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public ProductController(IProductService productService, ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int page = 1, int take = 4)
		{
			List<ProductVM> dbPaginatedDatas = await _productService.GetPaginatedDatasAsync(page, take);

			int pageCount = await GetPageCountAsync(take);

			Paginate<ProductVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);
			return View(paginatedDatas);
		}

		public async Task<int> GetPageCountAsync(int take)
		{
			int productCount = await _productService.GetCountAsync();

			return (int)Math.Ceiling((decimal)(productCount) / take);
		}

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return BadRequest();

			ProductDetailVM product = await _productService.GetByIdWithIncludesWithoutTrackingAsync((int)id);

			if (product is null) return NotFound();

			return View(product);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{

			ViewBag.categories = await GetCategoriesAsync();
			return View();

		}


		[HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {

            ViewBag.categories = await GetCategoriesAsync();

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            ProductVM existProduct = await _productService.GetByNameWithoutTrackingAsync(request.Name);

            if (existProduct is not null)
            {
                ModelState.AddModelError("Name", "This name already exists");

                return View(request);
            }

            foreach (var photo in request.Photos)
            {

                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "File can be only image format");
                    return View(request);
                }

                if (!photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photos", "File size can be max 200 kb");
                    return View(request);
                }
            }


            await _productService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
			
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

		[HttpGet]


        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.categories = await GetCategoriesAsync();


            if (id is null) return BadRequest();

            ProductDetailVM dbProduct = await _productService.GetByIdWithIncludesWithoutTrackingAsync((int)id);

            if (dbProduct is null) NotFound();

            return View(new ProductEditVM
            {
                Id = dbProduct.Id,
                Name = dbProduct.Name,
                Price = dbProduct.Price,
                Description = dbProduct.Description,
                CategoryId = dbProduct.CategoryId,
                Images = dbProduct.Images
            });
        }

		[HttpPost]
		[ValidateAntiForgeryToken]

		public async Task<IActionResult> Edit(int? id, ProductEditVM request)
		{
			ViewBag.categories = await GetCategoriesAsync();

			if (id is null) return BadRequest();

			ProductDetailVM dbProduct = await _productService.GetByIdWithIncludesWithoutTrackingAsync((int)id);

			if (dbProduct is null) return NotFound();

			request.Images = dbProduct.Images;

			if (!ModelState.IsValid)
			{
				return View(request);
			}

			ProductVM existProduct = await _productService.GetByNameWithoutTrackingAsync(request.Name);


			if (request.Photos != null)
			{
				foreach (var photo in request.Photos)
				{
					if (!photo.CheckFileType("image/"))
					{
						ModelState.AddModelError("Photos", "File can only be in image format");
						return View(request);

					}

					if (!photo.CheckFileSize(200))
					{
						ModelState.AddModelError("Photos", "File size can be max 200 kb");
						return View(request);
					}
				}
			}


			if (existProduct is not null)
			{
				if (existProduct.Id == request.Id)
				{
					await _productService.EditAsync(request);

					return RedirectToAction(nameof(Index));
				}

				ModelState.AddModelError("Name", "This name already exists");
				return View(request);
			}

			await _productService.EditAsync(request);

			return RedirectToAction(nameof(Index));

		}

		[HttpPost]

		public async Task<IActionResult> DeleteProductImage(int id)
		{
			await _productService.DeleteProductImageAsync(id);

			return Ok();
		}


		private async Task<SelectList> GetCategoriesAsync()
		{
			return new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
		}

	}
}
