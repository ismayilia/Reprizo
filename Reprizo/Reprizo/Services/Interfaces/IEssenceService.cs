using Reprizo.Areas.Admin.ViewModels.Essence;

namespace Reprizo.Services.Interfaces
{
    public interface IEssenceService
    {
        Task<List<EssenceVM>> GetAllAsync();
    }
}
