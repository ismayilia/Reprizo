using Reprizo.Areas.Admin.ViewModels.Layout;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
	public class LayoutService : ILayoutService
	{
		private readonly IBasketService _basketService;
		private readonly IWishlistService _wishlistService;
		private readonly ISettingService _settingService;

		public LayoutService(IBasketService basketService, ISettingService settingService, IWishlistService wishlistService)
		{
			_basketService = basketService;
			_settingService = settingService;
			_wishlistService = wishlistService;
		}
		public HeaderVM GetHeaderDatas()
		{
			Dictionary<string, string> settingDatas = _settingService.GetSettings();
			int basketCount = _basketService.GetCount();
			int wishlistCount = _wishlistService.GetCount();
			return new HeaderVM
			{
				BasketCount = basketCount,
				WishlistCount = wishlistCount,
				Logo = settingDatas["HeaderLogo"]
			};
		}
	}
}
