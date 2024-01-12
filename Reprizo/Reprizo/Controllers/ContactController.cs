using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
	public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IContactService _contactService;
        public ContactController(ISettingService settingService, IContactService contactService)
        {
            _settingService = settingService;
            _contactService = contactService;

        }
        public IActionResult Index()
        {
            Dictionary<string,string> settingDatas = _settingService.GetSettings();

            ContactVM model = new()
            {
                MailOne = settingDatas["MailOne"],
                MailTwo = settingDatas["MailTwo"],
                PhoneOne = settingDatas["PhoneOne"],
                PhoneTwo = settingDatas["PhoneTwo"],
                WeekDays = settingDatas["WeekDays"],
                Weekends = settingDatas["Weekends"],
                Address = settingDatas["Address"],
                ContactBanner = settingDatas["ContactBanner"]
            };
            return View(model);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateMessage(ContactMessageCreateVM request)
		{

			await _contactService.CreateAsync(request);

			return RedirectToAction("Index", "Contact");

		}
	}
}
