using Newtonsoft.Json;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers.Responses;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
	public class WishlistService : IWishlistService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IProductService _productService;
        public WishlistService(IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
			_productService = productService;
        }

        public int AddWishlist(int id, ProductVM product)
		{
			List<WishlistVM> wishlist;

			if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
			{
				wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
			}
			else
			{
				wishlist = new List<WishlistVM>();
			}

            

            WishlistVM existProducts = wishlist.FirstOrDefault(m => m.Id == product.Id);

			if (existProducts is null)
			{
				wishlist.Add(new WishlistVM { Id = product.Id});
			}
			

			_httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

			return wishlist.Count();
		}

		public async Task<DeleteWishlistItemResponse> DeleteItem(int id)
		{

			List<WishlistVM> wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);

			WishlistVM wishlistItem = wishlist.FirstOrDefault(m => m.Id == id);

			wishlist.Remove(wishlistItem);

			_httpContextAccessor.HttpContext.Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

			return new DeleteWishlistItemResponse
			{
				Count = wishlist.Count()
			};
		}

		public int GetCount()
		{
			List<WishlistVM> wishlist;

			if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
			{
				wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
			}
			else
			{
				wishlist = new List<WishlistVM>();

			}

			return wishlist.Count();
		}

        public async Task<List<WishlistDetailVM>> GetWishlistDatasAsync()
        {
            List<WishlistVM> wishlist;

            if (_httpContextAccessor.HttpContext.Request.Cookies["wishlist"] != null)
            {
                wishlist = JsonConvert.DeserializeObject<List<WishlistVM>>(_httpContextAccessor.HttpContext.Request.Cookies["wishlist"]);
            }
            else
            {
                wishlist = new List<WishlistVM>();

            }

            List<WishlistDetailVM> wishlistDetails = new();
            foreach (var item in wishlist)
            {
                ProductVM existProduct = await _productService.GetByIdWithIncludesAsync(item.Id);

                wishlistDetails.Add(new WishlistDetailVM
                {
                    Id = existProduct.Id,
                    Name = existProduct.Name,
                    Price = existProduct.Price,
                    Image = existProduct.Image
                });
            }
            return wishlistDetails;
        }
    }
}
