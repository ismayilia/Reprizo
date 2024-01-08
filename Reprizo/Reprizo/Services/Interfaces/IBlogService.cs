using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Areas.Admin.ViewModels.Product;

namespace Reprizo.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take);
        Task<List<BlogVM>> GetAllAsync();
        Task<int> GetCountAsync();
    }
}
