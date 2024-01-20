using Reprizo.Areas.Admin.ViewModels.Subscribe;

namespace Reprizo.Services.Interfaces
{
	public interface ISubscribeService
    {
        Task CreateAsync(SubscribeCreateVM subscribe);
		Task<List<SubscribeVM>> GetAllAsync();
		Task<SubscribeVM> GetByEmailAsync(string email);
		Task DeleteAsync(int id);
	}
}
