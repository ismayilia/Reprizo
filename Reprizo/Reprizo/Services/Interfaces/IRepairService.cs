using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Repair;

namespace Reprizo.Services.Interfaces
{
    public interface IRepairService
    {
        Task<RepairVM> GetDataAsync();
    }
}
