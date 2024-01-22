using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Helpers.Responses;
using Reprizo.Models;

namespace Reprizo.Services.Interfaces
{
    public interface IBasketService
	{
		void AddBasket(int id, ProductVM product);
		int GetCount();
		Task<List<BasketDetailVM>> GetBasketDatasAsync();
        Task<CountPlusAndMinus> PlusIcon(int id);
        Task<CountPlusAndMinus> MinusIcon(int id);
        Task<DeleteBasketItemResponse> DeleteItem(int id);
		List<BasketVM> GetDatasFromCoockies();
		Task<Basket> GetByUserIdAsync(string userId);
		Task<List<BasketProduct>> GetAllByBasketIdAsync(int? basketId);

	}
}
