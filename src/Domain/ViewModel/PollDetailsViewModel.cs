using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.ViewModel
{
    public class PollDetailsViewModel
    {
        public int PollID { get; set; }
        public string Question { get; set; }
        public IEnumerable<PollOption> PollOption { get; set; }
    }
}
