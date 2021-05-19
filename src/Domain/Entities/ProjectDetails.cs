
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComeTogether.Domain.Entities
{
   public class ProjectDetails
    {
        public ProjectDetails()
        {
            Projects = new HashSet<Project>();
        }

      

        public ICollection<Project> Projects { get; private set; }
        [Key]
        public int ProjectId { get; set; }
         public int FounderId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string RisksAndChallenges{ get; set; }
        public string Background { get; set; }
        public int StartBudget { get; set; }
        public DateTime StartDate { get; set; }
 
        public int CPO { get; set; }
        public int CAC { get; set; }
        public int ROMI { get; set; }
        public int ROI { get; set; }
        public int ROAS { get; set; }
        public int ARPU { get; set; }
        public int AOV { get; set; }
        public int LTV { get; set; }
      



    }
}
