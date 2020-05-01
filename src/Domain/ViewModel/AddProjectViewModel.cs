using ComeTogether.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.ViewModel
{
   public class AddProjectViewModel
    {
    
        public string ProjectName { get; set; }
      
        public int? CategoryId { get; set; }


        public Category Category { get; set; }
        public ApplicationUser Founder { get; set; }

        public ICollection<ProjectDetails> ProjectDetails { get; private set; }
      //  public ICollection<Partner> Partners { get; private set; }
    }
}
