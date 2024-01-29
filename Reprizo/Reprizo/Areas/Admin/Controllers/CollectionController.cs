using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Collection;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
	public class CollectionController : MainController
    {
        private readonly ICollectionService _collectionService;
		private readonly IMapper _mapper;

        public CollectionController(ICollectionService collectionService, IMapper mapper)
        {
            _collectionService = collectionService;
			_mapper	 = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _collectionService.GetDataAsync());
        }

		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			CollectionVM collection = await _collectionService.GetByIdAsync((int)id);

			if (collection is null) return RedirectToAction("Index", "Error");

			return View(collection);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			CollectionVM collection = await _collectionService.GetByIdAsync((int)id);

			if (collection is null) return RedirectToAction("Index", "Error");

			CollectionEditVM collectionEditVM = _mapper.Map<CollectionEditVM>(collection);

			return View(collectionEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CollectionEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            CollectionVM dbCollection = await _collectionService.GetByIdAsync((int)id);

            if (dbCollection is null) return RedirectToAction("Index", "Error");

            if (!ModelState.IsValid)
            {
                return View(request);
            }


            await _collectionService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
