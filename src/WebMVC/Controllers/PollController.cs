using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Domain.ViewModel;
using ComeTogether.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class PollController : Controller
    {
        private readonly IPollManager _pollManager;

        public PollController(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPoll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPoll(AddPollViewModel poll)
        {
            if (ModelState.IsValid)
            {
                if (_pollManager.AddPoll(poll))
                {
                    ViewBag.Message = "Poll added successfully!";
                    //ASPNETCoreSignalRDemo.Hubs.PollHub.FetchPoll();
                }
            }
            return View(poll);
        }
    }
}
