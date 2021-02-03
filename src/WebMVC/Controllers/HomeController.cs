using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Globalization;
using WebMVC.Models;

using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private static readonly IStringLocalizer<BaseController> _localizer;
      //  private static readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(ILogger<HomeController> logger) 
        {
           
            _logger = logger;
          
        }

        public IActionResult Index()
        {
            return View();
        }
     

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Chat()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
