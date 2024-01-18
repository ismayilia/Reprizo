using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class BlogService : IBlogService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public BlogService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(BlogCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
            string path = _env.GetFilePath("assets/img/blog", fileName);

            var data = _mapper.Map<Blog>(request);

            data.Image = fileName;

            await _context.Blogs.AddAsync(data);
            await _context.SaveChangesAsync();
            await request.Photo.SaveFileAsync(path);
        }

		public async Task DeleteAsync(int id)
		{
			Blog blog = await _context.Blogs.Where(m => m.Id == id).FirstOrDefaultAsync();
			_context.Blogs.Remove(blog);
			await _context.SaveChangesAsync();

			string path = _env.GetFilePath("assets/img/blog", blog.Image);

			if (File.Exists(path))
			{
				File.Delete(path);
			}
			
		}

        public async Task EditAsync(BlogEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img/blog", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img/blog", fileName);

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

            Blog dbBlog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbBlog);

            dbBlog.Image = fileName;

            _context.Blogs.Update(dbBlog);

            await _context.SaveChangesAsync();
        }

        public async Task<List<BlogVM>> GetAllAsync()
        {
            var datas = await _context.Blogs.ToListAsync();

            return _mapper.Map<List<BlogVM>>(datas);
        }

        public async Task<BlogVM> GetByIdAsync(int id)
        {
            Blog data = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<BlogVM>(data);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public async Task<List<BlogVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Blog> datas = await _context.Blogs
                                                   .Skip((page * take) - take)
                                                   .Take(take)
                                                   .ToListAsync();

            return _mapper.Map<List<BlogVM>>(datas);
        }
    }
}
