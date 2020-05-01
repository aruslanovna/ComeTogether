
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComeTogether.Domain.Entities
{
    public class Project
    {
        public Project()
        {
           
            Partners = new HashSet<ApplicationUser>();
        }

        public int ProjectId { get; set; }
        [StringLength(30, ErrorMessage = "Max length is 30 symbols")]
        public string ProjectName { get; set; }
        public string FounderId { get; set; }
        public int? CategoryId { get; set; }


        public Category Category { get; set; }
        public ApplicationUser Founder { get; set; }
        [StringLength(30, ErrorMessage = "Max length is 30 symbols")]
        public string ShortDescription { get; set; }
        [StringLength(350, MinimumLength = 30, ErrorMessage = "Max length is 350 symbols")]
        public string FullDescription { get; set; }
        public string RisksAndChallenges { get; set; }
        public string Background { get; set; }
        public int StartBudget { get; set; }
        public DateTime StartDate { get; set; }
        // public ProjectDetails ProjectDetails { get; private set; }
        public ICollection<ApplicationUser> Partners { get; private set; }

    }
}