using Reprizo.Areas.Admin.ViewModels.Contact;

namespace Reprizo.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactMessageCreateVM contact);

    }
}
