using Reprizo.Areas.Admin.ViewModels.Team;

namespace Reprizo.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<TeamVM> GetByIdAsync(int id);
        Task EditAsync(TeamEditVM request);
    }
}
