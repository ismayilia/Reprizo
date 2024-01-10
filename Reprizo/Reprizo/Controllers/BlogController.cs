using Microsoft.AspNetCore.Mvc;
using Reprizo.Areas.Admin.ViewModels.Blog;
using Reprizo.Areas.Admin.ViewModels.Product;
using Reprizo.Data;
using Reprizo.Helpers;
using Reprizo.Services;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
	public class BlogController : Controller
	{

        private readonly AppDbContext _context;
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;

        public BlogController(AppDbContext context, 
                                                   IBlogService blogService,
                                                   ISettingService settingService)
        {
            _context = context;
            _blogService = blogService;
            _settingService = settingService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            List<BlogVM> dbPaginatedDatas = await _blogService.GetPaginatedDatasAsync(page, take);
            List<BlogVM> blogs = await _blogService.GetAllAsync();
            int pageCount = await GetPageCountAsync(take);
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.BlogBanner= settingDatas["BlogBanner"];
            ViewBag.Blogs= blogs.OrderByDescending(m=>m.Id).Take(2);

            Paginate<BlogVM> paginatedDatas = new(dbPaginatedDatas, page, pageCount);

            return View(paginatedDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int blogCount = await _blogService.GetCountAsync();

            return (int)Math.Ceiling((decimal)(blogCount) / take);
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            BlogVM blog = await _blogService.GetByIdAsync(id);
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.BlogDetailBanner = settingDatas["BlogDetailBanner"];

            return View(blog);
        }
    }
}
