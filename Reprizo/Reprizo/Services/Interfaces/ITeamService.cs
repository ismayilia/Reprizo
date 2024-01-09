using Reprizo.Areas.Admin.ViewModels.Team;

namespace Reprizo.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
    }
}
