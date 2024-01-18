using Reprizo.Areas.Admin.ViewModels.Essence;

namespace Reprizo.Services.Interfaces
{
	public interface IEssenceService
    {
        Task<List<EssenceVM>> GetAllAsync();
        Task<EssenceVM> GetByIdAsync(int id);
        Task EditAsync(EssenceEditVM request);
    }
}
