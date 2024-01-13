using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductVM>> GetAllAsync()
        {
            return _mapper.Map<List<ProductVM>>(await _context.Products.Include(m => m.Category)
                                                                       .Include(m => m.Images)
                                                                       .ToListAsync());
        }

        public async Task<ProductVM> GetByIdWithIncludesAsync(int id)
        {
            Product data = await _context.Products.Include(m => m.Category)
                                                   .Include(m => m.Images)
                                                   .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<ProductVM>(data);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }
		public async Task<int> GetCountByCategoryAsync(int id)
		{
			return await _context.Products.Where(m => m.CategoryId == id)
															.Include(m => m.Category)
															.Include(m => m.Images).CountAsync();
		}

		public async Task<int> GetCountBySearch(string searchText)
		{
			return await _context.Products.Include(m => m.Images)
												 .Include(m => m.Category)
												 .OrderByDescending(m => m.Id)
												 .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
												 .CountAsync();

		}

		public async Task<List<ProductVM>> GetPaginatedDatasAsync(int page, int take)
        {
            List<Product> products = await _context.Products.Include(m => m.Category)
                                                             .Include(m => m.Images)
                                                             .Skip((page * take) - take)
                                                             .Take(take)
                                                             .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);
        }

        public async Task<List<ProductVM>> GetPaginatedDatasByCategory(int id, int page, int take)
        {
            List<Product> products = await _context.Products.Where(m => m.CategoryId == id)
                                                            .Include(m => m.Category)
                                                            .Include(m => m.Images)
                                                            .Skip((page * take) - take)
                                                            .Take(take)
                                                            .ToListAsync();
            return _mapper.Map<List<ProductVM>>(products);

        }

		public async Task<List<ProductVM>> OrderByNameAsc(int page, int take)
		{
			var dbProducts = await _context.Products.Include(m => m.Images)
                                                                            .OrderBy(p => p.Name)
                                                                            .Skip((page * take) - take)
			                                                                .Take(take)
                                                                            .ToListAsync();
			return _mapper.Map<List<ProductVM>>(dbProducts);
		}

		public async Task<List<ProductVM>> OrderByNameDesc(int page, int take)
		{
			var dbProducts = await _context.Products.Include(m => m.Images)
																			.OrderByDescending(p => p.Name)
																			.Skip((page * take) - take)
																			.Take(take)
																			.ToListAsync();
			return _mapper.Map<List<ProductVM>>(dbProducts);
		}

		public async Task<List<ProductVM>> OrderByPriceAsc(int page, int take)
		{
			var dbProducts = await _context.Products.Include(m => m.Images)
																			.OrderBy(p => p.Price)
																			.Skip((page * take) - take)
																			.Take(take)
																			.ToListAsync();
			return _mapper.Map<List<ProductVM>>(dbProducts);
		}

		public async Task<List<ProductVM>> OrderByPriceDesc(int page, int take)
		{
			var dbProducts = await _context.Products.Include(m => m.Images)
																			.OrderByDescending(p => p.Price)
																			.Skip((page * take) - take)
																			.Take(take)
																			.ToListAsync();
			return _mapper.Map<List<ProductVM>>(dbProducts);
		}

		public async Task<List<ProductVM>> SearchAsync(string searchText, int page, int take)
        {
            var dbProducts = await _context.Products.Include(m => m.Images)
                                                 .Include(m => m.Category)
                                                 .OrderByDescending(m => m.Id)
                                                 .Where(m => m.Name.ToLower().Trim().Contains(searchText.ToLower().Trim()))
                                                 .Skip((page * take) - take)
                                                 .Take(take)
                                                 .ToListAsync();

            return _mapper.Map<List<ProductVM>>(dbProducts);
        }
    }
}
