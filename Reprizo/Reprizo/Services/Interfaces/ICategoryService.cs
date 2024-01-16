using Reprizo.Areas.Admin.ViewModels.Category;

namespace Reprizo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
        Task CreateAsync(CategoryCreateVM request);
        Task DeleteAsync(int id);
        Task<CategoryVM> GetByNameWithoutTrackingAsync(string name);
        Task EditAsync(CategoryEditVM request);
    }
}
