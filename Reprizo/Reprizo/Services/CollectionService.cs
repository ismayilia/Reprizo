using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Areas.Admin.ViewModels.Slider;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Data;

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
        public async Task<CollectionVM> GetDataAsync()
        {
            var data = await _context.Collections.FirstOrDefaultAsync();

            return _mapper.Map<CollectionVM>(data);
        }
    }
}
