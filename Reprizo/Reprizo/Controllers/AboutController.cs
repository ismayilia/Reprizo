﻿using Microsoft.AspNetCore.Mvc;

namespace Reprizo.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
