using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class BlogService : IBlogService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public BlogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<List<BlogVM>> GetAllAsync()
        {
            var datas = await _context.Blogs.ToListAsync();

            return _mapper.Map<List<BlogVM>>(datas);
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
