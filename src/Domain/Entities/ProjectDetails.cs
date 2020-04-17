using ComeTogether.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComeTogether.Domain.Entities
{
   public class ProjectDetails: AuditableEntity
    {
        [Key]
        public int ProjectId { get; set; }
            public int FounderId { get; set; }
          
          //  public Order Order { get; set; }
            public Project Project { get; set; }
        
    }
}
