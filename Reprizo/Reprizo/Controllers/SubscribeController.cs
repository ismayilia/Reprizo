using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Subscribe;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExistEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var existEmail = await _subscribeService.GetByEmailAsync(subscribe.Email);

            if (existEmail is not null)
            {
                return RedirectToAction(nameof(ExistEmail));
            }
            
            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction(nameof(Index));
        }
    }
}
