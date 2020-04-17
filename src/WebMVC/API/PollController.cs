using ComeTogether.Domain.ViewModel;
using ComeTogether.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.API
{
    [Route("api/[controller]")]
    public class PollController : Controller
    {
        private readonly IPollManager _pollManager;

        public PollController(IPollManager pollManager)
        {
            _pollManager = pollManager;
        }

        [HttpGet]
        public IEnumerable<PollDetailsViewModel> Get()
        {
            var res = _pollManager.GetActivePoll();
            return res.ToList();
        }

    }
}
