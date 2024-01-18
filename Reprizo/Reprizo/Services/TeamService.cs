using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Team;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class TeamService : ITeamService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task EditAsync(TeamEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img/about", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img/about", fileName);

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await request.Photo.SaveFileAsync(newPath);

            }
            else
            {
                fileName = request.Image;
            }

            Team dbTeam = await _context.Teams.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbTeam);

            dbTeam.Image = fileName;

            _context.Teams.Update(dbTeam);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TeamVM>> GetAllAsync()
        {
            var datas = await _context.Teams.ToListAsync();

            return _mapper.Map<List<TeamVM>>(datas);
        }

        public async Task<TeamVM> GetByIdAsync(int id)
        {
            Team data = await _context.Teams.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<TeamVM>(data);
        }
    }
    
}
