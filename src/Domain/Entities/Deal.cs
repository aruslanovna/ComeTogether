using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
   public class Deal
    {
        public string Id { get; set; }
        
        public Project Project { get; set; }
        public Partner Partner { get; set; }
    }
}
