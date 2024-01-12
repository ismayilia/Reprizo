using AutoMapper;
using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(AppDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateAsync(ContactMessageCreateVM contact)
        {
            var data = _mapper.Map<ContactMessage>(contact);


            await _context.ContactMessages.AddAsync(data);
            await _context.SaveChangesAsync();
        }
    }
}
