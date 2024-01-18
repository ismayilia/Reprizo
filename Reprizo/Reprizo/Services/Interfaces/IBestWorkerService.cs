using Reprizo.Areas.Admin.ViewModels.BestWorker;

namespace Reprizo.Services.Interfaces
{
    public interface IBestWorkerService
    {
        Task<BestWorkerVM> GetDataAsync();
        Task<BestWorkerVM> GetByIdAsync(int id);
        Task EditAsync(BestWorkerEditVM request);
    }
}
