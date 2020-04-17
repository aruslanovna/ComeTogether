using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{

    public partial class PollOption
    {
        public int PollOptionId { get; set; }
        public int PollId { get; set; }
        public string Answers { get; set; }
        public int Vote { get; set; }

        public virtual Poll Poll { get; set; }
    }
}
