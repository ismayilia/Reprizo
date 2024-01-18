using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.BestWorker;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Feature;
using Reprizo.Data;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class BestWorkerService : IBestWorkerService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BestWorkerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task EditAsync(BestWorkerEditVM request)
        {
            var dbBestWorker = await _context.BestWorkers.FirstOrDefaultAsync(m => m.Id == request.Id);

            _mapper.Map(request, dbBestWorker);

            _context.BestWorkers.Update(dbBestWorker);

            await _context.SaveChangesAsync();
        }

        public async Task<BestWorkerVM> GetByIdAsync(int id)
        {
            var data = await _context.BestWorkers.FirstOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<BestWorkerVM>(data);
        }

        public async Task<BestWorkerVM> GetDataAsync()
        {
            var data = await _context.BestWorkers.FirstOrDefaultAsync();

            return _mapper.Map<BestWorkerVM>(data);
        }
    }
}
