using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Helpers.Extensions;
using Reprizo.Services.Interfaces;

namespace Reprizo.Areas.Admin.Controllers
{
    public class BlogController : MainController
    {
        private readonly IBlogService _blogService;
		private readonly IMapper _mapper;
		public BlogController(IBlogService blogService, IMapper mapper)
        {
            _blogService = blogService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			BlogVM blog = await _blogService.GetByIdAsync((int)id);

			if (blog is null) return RedirectToAction("Index", "Error");

			return View(blog);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)

        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "File can be only image format");
                return View();
            }

            if (!request.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", "File size can be max 500 kb");
                return View();
            }

            await _blogService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			await _blogService.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return RedirectToAction("Index", "Error");

			BlogVM blog = await _blogService.GetByIdAsync((int)id);

			if (blog is null) return RedirectToAction("Index", "Error");

			BlogEditVM blogEditVM = _mapper.Map<BlogEditVM>(blog);


			return View(blogEditVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            BlogVM dbBlog = await _blogService.GetByIdAsync((int)id);

            if (dbBlog is null) return RedirectToAction("Index", "Error");


            request.Image = dbBlog.Image;

            if (!ModelState.IsValid)
            {
                return View(request);

            }

            if (request.Photo is not null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }
                if (!request.Photo.CheckFileSize(500))
                {
                    ModelState.AddModelError("Photo", "File size can be max 500 kb");
                    return View(request);
                }
            }

            await _blogService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }


    }
}
