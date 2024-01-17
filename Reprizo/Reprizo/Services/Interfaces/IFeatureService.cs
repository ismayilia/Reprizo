using Reprizo.Areas.Admin.ViewModels.Feature;

namespace Reprizo.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<FeatureVM> GetDataAsync();
		Task<FeatureVM> GetByIdAsync(int id);
        Task EditAsync(FeatureEditVM request);
    }
}
