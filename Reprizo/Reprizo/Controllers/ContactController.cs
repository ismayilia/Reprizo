using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Contact;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class ContactController : Controller
    {
        //private readonly IRepairService _repairService;
        //public ContactController(IRepairService repairService)
        //{
        //    _repairService = repairService;
        //}
        public IActionResult Index()
        {
            //RepairVM repair = await _repairService.GetDataAsync();

            //ContactVM model = new()
            //{
            //    Repair = repair
            //};
            return View();
        }
    }
}
