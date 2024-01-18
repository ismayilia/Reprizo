using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class RepairService : IRepairService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public RepairService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task EditAsync(RepairEditVM request)
        {
            string fileName;

            if (request.Photo is not null)
            {
                string oldPath = _env.GetFilePath("assets/img", request.Image);
                fileName = $"{Guid.NewGuid()}-{request.Photo.FileName}";
                string newPath = _env.GetFilePath("assets/img", fileName);

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

            Repair dbRepair = await _context.Repairs.FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbRepair);

            dbRepair.Image = fileName;

            _context.Repairs.Update(dbRepair);

            await _context.SaveChangesAsync();
        }

        public async Task<RepairVM> GetByIdAsync(int id)
		{
			Repair data = await _context.Repairs.FirstOrDefaultAsync(m => m.Id == id);
			return _mapper.Map<RepairVM>(data);
		}

		public async Task<RepairVM> GetDataAsync()
        {
            var data = await _context.Repairs.FirstOrDefaultAsync();

            return _mapper.Map<RepairVM>(data);
        }
    }
}
