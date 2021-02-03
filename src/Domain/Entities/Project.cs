
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ComeTogether.Domain.Entities
{
    public class Project
    {
        public Project()
        {
           
            Deals = new HashSet<Deal>();
        }

        public int ProjectId { get; set; }
      
        public string ProjectName { get; set; }
        public string FounderId { get; set; }
        public int? CategoryId { get; set; }
        public byte[] Photo { get; set; }

        public Category Category { get; set; }
        public ApplicationUser Founder { get; set; }
       
        public string ShortDescription { get; set; }
       
        public string FullDescription { get; set; }
        public string RisksAndChallenges { get; set; }
        public string Background { get; set; }
        public int StartBudget { get; set; }
        public DateTime StartDate { get; set; }
        // public ProjectDetails ProjectDetails { get; private set; }
       // public ICollection<ApplicationUser> Partners { get; private set; }
        public ICollection<Deal> Deals { get; set; }

    }
}