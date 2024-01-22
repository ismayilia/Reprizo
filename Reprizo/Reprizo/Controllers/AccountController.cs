using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reprizo.Areas.Admin.ViewModels.Account;
using Reprizo.Areas.Admin.ViewModels.Shop;
using Reprizo.Data;
using Reprizo.Helpers.Enums;
using Reprizo.Models;
using Reprizo.Services.Interfaces;

namespace Reprizo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWishlistService _wishlistService;
        private readonly AppDbContext _context;


        public AccountController(ISettingService settingService,
                                UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IWishlistService wishlistService,
                                 AppDbContext context)
        {
            _settingService = settingService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _wishlistService = wishlistService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.RegisterBanner = settingDatas["RegisterBanner"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.RegisterBanner = settingDatas["RegisterBanner"];

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,

            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);

            }

            var createdUser = await _userManager.FindByNameAsync(user.UserName);

            await _userManager.AddToRoleAsync(createdUser, Roles.Member.ToString());

            return RedirectToAction(nameof(Login));
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.LoginBanner = settingDatas["LoginBanner"];
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.LoginBanner = settingDatas["LoginBanner"];

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (dbUser is null)
            {
                dbUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (dbUser is null)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, request.IsRememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View();

            }

            List<WishlistVM> wishlist = new();

            Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(dbUser.Id);

            if (dbWishlist is not null)
            {
                List<WishlistProduct> wishlistProducts = await _wishlistService.GetAllByWishlistIdAsync(dbWishlist.Id);

                foreach (var item in wishlistProducts)
                {
                    wishlist.Add(new WishlistVM
                    {
                        Id = item.ProductId
                    });
                }

                Response.Cookies.Append("wishlist", JsonConvert.SerializeObject(wishlist));

            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string userId)
        {
            await _signInManager.SignOutAsync();

            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCoockies();
			Wishlist dbWishlist = await _wishlistService.GetByUserIdAsync(userId);

			if (wishlist.Count != 0)
            {
                if (dbWishlist == null)
                {
                    dbWishlist = new()
                    {
                        AppUserId=userId,
                        WishlistProducts = new List<WishlistProduct>()
                        
                    };

                    foreach (var item in wishlist)
                    {
                        dbWishlist.WishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.Id,
                            WishlistId = dbWishlist.Id
                        });

                    }

                    await _context.Wishlists.AddAsync(dbWishlist);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    List<WishlistProduct> wishlistProducts = new();

                    foreach (var item in wishlist)
                    {
                        wishlistProducts.Add(new WishlistProduct()
                        {
                            ProductId = item.Id,
                            WishlistId = dbWishlist.Id
                        });
                    }

                    dbWishlist.WishlistProducts = wishlistProducts;

                    _context.SaveChanges();

                }

                Response.Cookies.Delete("wishlist");
            }
            else
            {
                _context.Wishlists.Remove(dbWishlist);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();

            ViewBag.ForgotBanner = settingDatas["ForgotBanner"];
            return View();
        }



        //Create Roles Method

        //[HttpGet]
        //public async Task<IActionResult> CreateRoles()
        //{
        //	foreach (var role in Enum.GetValues(typeof(Roles)))
        //	{
        //		if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //		{
        //			await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //		}
        //	}
        //	return Ok();
        //}
    }
}
