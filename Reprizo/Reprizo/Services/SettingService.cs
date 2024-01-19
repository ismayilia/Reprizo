using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Setting;
using Reprizo.Data;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
	public class SettingService : ISettingService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public SettingService(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
         }

        public async Task EditAsync(SettingEditVM setting)
        {
            if (setting.Value.Contains("jpg") || setting.Value.Contains("png") || setting.Value.Contains("jpeg"))
            {
                string oldPath = _env.GetFilePath("assets/img/settings", setting.Value);

                string fileName = $"{Guid.NewGuid()}-{setting.Photo.FileName}";

                string newPath = _env.GetFilePath("assets/img/settings", fileName);

                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                dbSetting.Value = fileName;
                _context.Settings.Update(dbSetting);
                await _context.SaveChangesAsync();

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                await setting.Photo.SaveFileAsync(newPath);
            }
            else
            {
                Setting dbSetting = await _context.Settings.FirstOrDefaultAsync(m => m.Id == setting.Id);

                _mapper.Map(setting, dbSetting);

                _context.Settings.Update(dbSetting);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Dictionary<string, string> GetSettings()
		{
			return _context.Settings.AsEnumerable()
									.ToDictionary(m => m.Key, m => m.Value);
		}
	}
}
