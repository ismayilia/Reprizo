using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(SliderCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
            string path = _env.GetFilePath("assets/img/slider", fileName);

            var data = _mapper.Map<Slider>(request);

            data.Image = fileName;

            await _context.Sliders.AddAsync(data);
            await _context.SaveChangesAsync();
            await request.Photo.SaveFileAsync(path);
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Sliders.Remove(slider) ;
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("assets/img/slider", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img/slider", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img/slider", fileName);

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



            Slider dbSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbSlider);

            dbSlider.Image = fileName;

            _context.Sliders.Update(dbSlider);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
            List<Slider> datas = await _context.Sliders.ToListAsync();

            return _mapper.Map<List<SliderVM>>(datas);
        }

        public async Task<SliderVM> GetByIdAsync(int id)
        {
			Slider data = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
			return _mapper.Map<SliderVM>(data);
		}
    }
}
