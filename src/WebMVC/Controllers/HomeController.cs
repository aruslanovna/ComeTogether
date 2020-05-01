using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ComeTogether.Domain.ViewModel;
using ComeTogether.Infrastructure.Repository;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebMVC.Models;
using WebMVC.Resources;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace WebMVC.Controllers
{
    public class HomeController : BaseController
    {
       
        private readonly ILogger<HomeController> _logger;
        private static readonly IStringLocalizer<BaseController> _localizer;
        private static readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(ILogger<HomeController> logger) :base(_localizer, _sharedLocalizer)
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
