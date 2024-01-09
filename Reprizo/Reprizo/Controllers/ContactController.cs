using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        public ContactController(ISettingService settingService)
        {
            _settingService = settingService;
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
    }
}
