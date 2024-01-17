using Reprizo.Areas.Admin.ViewModels.Slider;

namespace Reprizo.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderVM> GetByIdAsync(int id);
        Task CreateAsync(SliderCreateVM request);
        Task DeleteAsync(int id);
        Task EditAsync(SliderEditVM request);
    }
}
