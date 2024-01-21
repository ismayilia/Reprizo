using Newtonsoft.Json;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers.Responses;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
	public class BasketService : IBasketService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IProductService _productService;

		public BasketService(IHttpContextAccessor httpContextAccessor, IProductService productService)
		{
			_httpContextAccessor = httpContextAccessor;
			_productService = productService;

		}
		public void AddBasket(int id, ProductVM product)
		{
			List<BasketVM> basket;

			if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
			{
				basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			}
			else
			{
				basket = new List<BasketVM>();
			}

			BasketVM existProducts = basket.FirstOrDefault(m => m.Id == product.Id);

			if (existProducts is null)
			{
				basket.Add(new BasketVM { Id = product.Id, Count = 1 });
			}
			else
			{
				existProducts.Count++;

			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
		}

		public async Task<DeleteBasketItemResponse> DeleteItem(int id)
		{
			List<decimal> grandTotal = new();

			List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

			BasketVM basketItem = basket.FirstOrDefault(m => m.Id == id);

			basket.Remove(basketItem);

			foreach (var item in basket)
			{
				var product = await _productService.GetByIdWithIncludesAsync(item.Id);


				decimal total = item.Count * product.Price;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			return new DeleteBasketItemResponse
			{
				Count = basket.Sum(m => m.Count),
				GrandTotal = grandTotal.Sum()
			};
		}

		public async Task<List<BasketDetailVM>> GetBasketDatasAsync()
		{
			List<BasketVM> basket;

			if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
			{
				basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			}
			else
			{
				basket = new List<BasketVM>();

			}

			List<BasketDetailVM> basketDetailList = new();
			foreach (var item in basket)
			{
				ProductVM existProduct = await _productService.GetByIdWithIncludesAsync(item.Id);

				basketDetailList.Add(new BasketDetailVM
				{
					Id = existProduct.Id,
					Name = existProduct.Name,
					Price = existProduct.Price,
					Count = item.Count,
					Total = existProduct.Price * item.Count,
					Category = existProduct.CategoryName,
					Image = existProduct.Image
				});
			}
			return basketDetailList;
		}

		public int GetCount()
		{
			List<BasketVM> basket;

			if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
			{
				basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			}
			else
			{
				basket = new List<BasketVM>();

			}

			return basket.Sum(m => m.Count);
		}

		public async Task<CountPlusAndMinus> MinusIcon(int id)
		{
			List<decimal> grandTotal = new();

			List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
			BasketVM existProduct = basket.FirstOrDefault(m => m.Id == id);


			if (existProduct.Count > 1)
			{

				existProduct.Count--;


			}
			foreach (var item in basket)
			{

				var product = await _productService.GetByIdWithIncludesAsync(item.Id);

				decimal total = item.Count * product.Price;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			var basketItem = await _productService.GetByIdWithIncludesAsync(id);
			var productTotalPrice = existProduct.Count * basketItem.Price;
			return new CountPlusAndMinus
			{
				CountItem = existProduct.Count,
				GrandTotal = grandTotal.Sum(),
				ProductTotalPrice = productTotalPrice,
				CountBasket = basket.Sum(m => m.Count)
			};
		}

		public async Task<CountPlusAndMinus> PlusIcon(int id)
		{
			List<decimal> grandTotal = new();

			List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

			BasketVM existProduct = basket.FirstOrDefault(m => m.Id == id);

			existProduct.Count++;

			var basketItem = await _productService.GetByIdWithIncludesAsync(id);

			var productTotalPrice = existProduct.Count * basketItem.Price;

			foreach (var item in basket)
			{

				var product = await _productService.GetByIdWithIncludesAsync(item.Id);

				decimal total = item.Count * product.Price;

				grandTotal.Add(total);
			}

			_httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

			return new CountPlusAndMinus
			{
				CountItem = existProduct.Count,
				GrandTotal = grandTotal.Sum(),
				ProductTotalPrice = productTotalPrice,
				CountBasket = basket.Sum(m => m.Count)

			};
		}
	}
}