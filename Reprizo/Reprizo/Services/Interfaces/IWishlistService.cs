using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers.Responses;

namespace Reprizo.Services.Interfaces
{
	public interface IWishlistService
	{
		int AddWishlist(int id, ProductVM product);
		int GetCount();
        Task<List<WishlistDetailVM>> GetWishlistDatasAsync();
		Task<DeleteWishlistItemResponse> DeleteItem(int id);
	}
}
