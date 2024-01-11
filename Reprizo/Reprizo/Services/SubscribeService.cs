using AutoMapper;
using Reprizo.Areas.Admin.ViewModels.Subscribe;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SubscribeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateAsync(SubscribeCreateVM subscribe)
        {
            var data = _mapper.Map<Subscribe>(subscribe);

            await _context.Subscribes.AddAsync(data);

            await _context.SaveChangesAsync();
        }
    }
}
