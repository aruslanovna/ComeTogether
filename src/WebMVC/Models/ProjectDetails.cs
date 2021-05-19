using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class ProjectDetailsModel

    {
       
        public int DealId { get; set; }
        public int ProjectId { get; set; }
       
        public string ProjectName { get; set; }
        public string FounderId { get; set; }
        public int? CategoryId { get; set; }
        public int? CPO { get; set; }
        public int? CAC { get; set; }
        public int? ROMI { get; set; }
        public int? ROI { get; set; }
        public int? ROAS { get; set; }
        public int? ARPU { get; set; }
        public int? AOV { get; set; }
        public int? LTV { get; set; }
        public byte[] Photo { get; set; }
        public string Category { get; set; }
        public ApplicationUser Founder { get; set; }
        public string Country { get; set; }
        public string ShortDescription { get; set; }
      
        public string FullDescription { get; set; }
        public string RisksAndChallenges { get; set; }
        public string Background { get; set; }
        public int StartBudget { get; set; }
        public DateTime StartDate { get; set; }
     
        public string UserName { get; set; }
      
       public IQueryable<List<string>> PartnerUserName { get; set; }

         public ICollection<object> Partners { get; set; }

       
    }
}
