using Reprizo.Areas.Admin.ViewModels.Setting;
using Reprizo.Models;

namespace Reprizo.Services.Interfaces
{
	public interface ISettingService
	{
		Dictionary<string, string> GetSettings();
        Task<List<Setting>> GetAllAsync();

        Task<Setting> GetByIdAsync(int id);
        Task EditAsync(SettingEditVM setting);
    }
}
