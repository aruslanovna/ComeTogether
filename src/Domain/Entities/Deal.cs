using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
   public class Deal
    {
        public int DealId { get; set; }
        
        public int ProjectId { get; set; }
        public string Partner { get; set; }
    }
}
