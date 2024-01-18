using Reprizo.Areas.Admin.ViewModels.Collection;

namespace Reprizo.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<CollectionVM> GetDataAsync();
		Task<CollectionVM> GetByIdAsync(int id);
        Task EditAsync(CollectionEditVM request);
    }
}
