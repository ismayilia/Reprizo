using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var datas = await _context.Categories.Include(m=>m.Products).ToListAsync();

            return _mapper.Map<List<CategoryVM>>(datas);
        }

		public async Task<CategoryVM> GetByIdAsync(int id)
		{
            Category data = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<CategoryVM>(data);
		}
	}
}
