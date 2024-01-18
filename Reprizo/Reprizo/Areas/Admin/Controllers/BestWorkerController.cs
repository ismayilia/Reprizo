using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.BestWorker;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class BestWorkerController : MainController
    {
        private readonly IBestWorkerService _bestWorkerService;
        private readonly IMapper _mapper;
        public BestWorkerController(IBestWorkerService bestWorkerService, IMapper mapper)
        {
            _bestWorkerService = bestWorkerService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _bestWorkerService.GetDataAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            BestWorkerVM bestWorker = await _bestWorkerService.GetByIdAsync((int)id);

            if (bestWorker is null) return NotFound();

            return View(bestWorker);
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			BestWorkerVM bestWorker = await _bestWorkerService.GetByIdAsync((int)id);

			if (bestWorker is null) return NotFound();

			BestWorkerEditVM bestWorkerEditVM = _mapper.Map<BestWorkerEditVM>(bestWorker);

			return View(bestWorkerEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BestWorkerEditVM request)
        {
            if (id is null) return BadRequest();

            BestWorkerVM dbBestWorker = await _bestWorkerService.GetByIdAsync((int)id);

            if (dbBestWorker is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(request);
            }


            await _bestWorkerService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
