using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Essence;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class EssenceService : IEssenceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EssenceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<List<EssenceVM>> GetAllAsync()
        {
            var datas = await _context.Essences.ToListAsync();

            return _mapper.Map<List<EssenceVM>>(datas);
        }
    }
}
