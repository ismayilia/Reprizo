using Microsoft.AspNetCore.Mvc.RazorPages;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Models;

namespace Reprizo.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<ProductVM> GetByIdWithIncludesAsync(int id);
        Task<List<ProductVM>> GetPaginatedDatasByCategory(int id,int page, int take);
        Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take);
        Task<int> GetCountAsync();
        Task<int> GetCountByCategoryAsync(int id);
        Task<int> GetCountBySearch(string searchText);
        Task<List<ProductVM>> SearchAsync(string searchText, int page, int take);

    }
}
