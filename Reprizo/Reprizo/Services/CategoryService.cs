using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(CategoryCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
            string path = _env.GetFilePath("assets/img/category", fileName);

            var data = _mapper.Map<Category>(request);

            data.Image = fileName;

            await _context.Categories.AddAsync(data);
            await _context.SaveChangesAsync();
            await request.Photo.SaveFileAsync(path);
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _context.Categories.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("assets/img/category", category.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(CategoryEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img/category", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img/category", fileName);

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



            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbCategory);

            dbCategory.Image = fileName;

            _context.Categories.Update(dbCategory);

            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var datas = await _context.Categories.Include(m => m.Products).ToListAsync();

            return _mapper.Map<List<CategoryVM>>(datas);
        }

        public async Task<CategoryVM> GetByIdAsync(int id)
        {
            Category data = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<CategoryVM>(data);
        }

        public async Task<CategoryVM> GetByNameWithoutTrackingAsync(string name)
        {

            return _mapper.Map<CategoryVM>(await _context.Categories.AsNoTracking()
                                                         .FirstOrDefaultAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower()));

        }
    }
}
