using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Models;

namespace Reprizo.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdWithIncludesAsync(int id);
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
    }
}
