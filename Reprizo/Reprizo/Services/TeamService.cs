using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Areas.Admin.ViewModels.Team;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class TeamService : ITeamService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TeamService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<List<TeamVM>> GetAllAsync()
        {
            var datas = await _context.Teams.ToListAsync();

            return _mapper.Map<List<TeamVM>>(datas);
        }
    }
}
