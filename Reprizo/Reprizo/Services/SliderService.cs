using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SliderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
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
