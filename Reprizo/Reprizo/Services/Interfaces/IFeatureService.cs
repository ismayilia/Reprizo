using Reprizo.Areas.Admin.ViewModels.Feature;

namespace Reprizo.Services.Interfaces
{
    public interface IFeatureService
    {
        Task<FeatureVM> GetDataAsync();
    }
}
