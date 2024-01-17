using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public FeatureService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task EditAsync(FeatureEditVM request)
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

            Feature dbFeature = await _context.Features.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbFeature);

            dbFeature.Image = fileName;

            _context.Features.Update(dbFeature);

            await _context.SaveChangesAsync();
        }

        public async Task<FeatureVM> GetByIdAsync(int id)
		{
			Feature data = await _context.Features.FirstOrDefaultAsync(m => m.Id == id);
			return _mapper.Map<FeatureVM>(data);
		}

		public async Task<FeatureVM> GetDataAsync()
        {
            var data = await _context.Features.FirstOrDefaultAsync();

            return _mapper.Map<FeatureVM>(data);
        }
    }
}
