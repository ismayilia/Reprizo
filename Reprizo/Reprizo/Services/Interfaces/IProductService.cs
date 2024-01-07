using Reprizo.Areas.Admin.ViewModels.Product;

namespace Reprizo.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetByTakeWithIncludes(int take);
    }
}
