using AutoMapper;
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
    }
}
