using Reprizo.Areas.Admin.ViewModels.Category;

namespace Reprizo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAllAsync();
        Task<CategoryVM> GetByIdAsync(int id);
    }
}
