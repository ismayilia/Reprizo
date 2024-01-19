using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Models;

namespace Reprizo.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactMessageCreateVM contact);
        Task<List<ContactMessageVM>> GetAllMessagesAsync();
        Task DeleteAsync(int id);
        Task<ContactMessageVM> GetMessageByIdAsync(int id);

    }
}
