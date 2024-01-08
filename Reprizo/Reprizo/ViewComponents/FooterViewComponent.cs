using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Layout;

namespace Reprizo.ViewComponents
{
	public class FooterViewComponent :ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{

			return await Task.FromResult(View());
		}
	}
}
