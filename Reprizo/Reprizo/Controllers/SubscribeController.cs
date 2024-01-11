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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubscribe(SubscribeCreateVM subscribe)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            await _subscribeService.CreateAsync(subscribe);
            return RedirectToAction(nameof(Index));
        }
    }
}
