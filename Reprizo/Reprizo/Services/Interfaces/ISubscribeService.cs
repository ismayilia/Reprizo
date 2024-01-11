using Reprizo.Areas.Admin.ViewModels.Subscribe;

namespace Reprizo.Services.Interfaces
{
    public interface ISubscribeService
    {
        Task CreateAsync(SubscribeCreateVM subscribe);
    }
}
