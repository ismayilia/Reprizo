using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CollectionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task EditAsync(CollectionEditVM request)
        {

            var dbCollection = await _context.Collections.FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbCollection);

            _context.Collections.Update(dbCollection);

            await _context.SaveChangesAsync();
        }

        public async Task<CollectionVM> GetByIdAsync(int id)
		{
			var data = await _context.Collections.FirstOrDefaultAsync(m => m.Id == id);
			return _mapper.Map<CollectionVM>(data);
		}

		public async Task<CollectionVM> GetDataAsync()
        {
            var data = await _context.Collections.FirstOrDefaultAsync();

            return _mapper.Map<CollectionVM>(data);
        }
    }
}
