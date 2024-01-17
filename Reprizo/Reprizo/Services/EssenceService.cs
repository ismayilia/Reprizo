using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class EssenceService : IEssenceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EssenceService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task EditAsync(EssenceEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img", fileName);

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

            Essence dbEssence = await _context.Essences.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbEssence);

            dbEssence.Image = fileName;

            _context.Essences.Update(dbEssence);

            await _context.SaveChangesAsync();
        }

        public async Task<List<EssenceVM>> GetAllAsync()
        {
            var datas = await _context.Essences.ToListAsync();

            return _mapper.Map<List<EssenceVM>>(datas);
        }

        public async Task<EssenceVM> GetByIdAsync(int id)
        {
            Essence data = await _context.Essences.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<EssenceVM>(data);
        }
    }
}
