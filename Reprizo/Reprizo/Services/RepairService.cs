using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class RepairService : IRepairService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RepairService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<RepairVM> GetDataAsync()
        {
            var data = await _context.Repairs.FirstOrDefaultAsync();

            return _mapper.Map<RepairVM>(data);
        }
    }
}
