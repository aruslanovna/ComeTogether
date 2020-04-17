using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class Poll
    {
        public Poll()
        {
            PollOption = new HashSet<PollOption>();
        }

        public int PollId { get; set; }
        public string Question { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<PollOption> PollOption { get; set; }
    }
}
