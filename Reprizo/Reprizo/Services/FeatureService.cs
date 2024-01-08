using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FeatureService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<FeatureVM> GetDataAsync()
        {
            var data = await _context.Features.FirstOrDefaultAsync();

            return _mapper.Map<FeatureVM>(data);
        }
    }
}
