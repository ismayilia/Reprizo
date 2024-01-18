using Reprizo.Areas.Admin.ViewModels.Blog;

namespace Reprizo.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take);
        Task<List<BlogVM>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<BlogVM> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateVM request);
		Task DeleteAsync(int id);
        Task EditAsync(BlogEditVM request);
    }
}
