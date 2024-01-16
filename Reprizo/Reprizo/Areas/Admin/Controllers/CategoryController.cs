using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reprizo.Areas.Admin.ViewModels.Category;
using Reprizo.Helpers.Extensions;
using Reprizo.Models;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CategoryCreateVM request)

        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);

            if (existCategory != null)
            {
                ModelState.AddModelError("Name", "This Category already exists");
                return View();
            }

            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "File size can be max 200 kb");
                return View();
            }

            await _categoryService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            CategoryVM category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            CategoryEditVM categoryEditVM = _mapper.Map<CategoryEditVM>(category);


            return View(categoryEditVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            CategoryVM dbCategory = await _categoryService.GetByIdAsync((int)id);

            if (dbCategory is null) return NotFound();


            request.Image = dbCategory.Image;

            if (!ModelState.IsValid)
            {
                return View(request);

            }


            CategoryVM existCategory = await _categoryService.GetByNameWithoutTrackingAsync(request.Name);

            if (existCategory != null)
            {
                if (existCategory.Id == request.Id)
                {
                    await _categoryService.EditAsync(request);

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("Name", "This Tag already exists");
                return View(request);
            }




            if (request.Photo is not null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }
                if (!request.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(request);
                }
            }


            await _categoryService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
