
using System.Collections.Generic;

namespace ComeTogether.Domain.Entities
{
    public class Project
    {
        public Project()
        {
            ProjectDetails = new HashSet<ProjectDetails>();
            Partners = new HashSet<Partner>();
        }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string FounderId { get; set; }
        public int? CategoryId { get; set; }


        public Category Category { get; set; }
        public Founder Founder { get; set; }
   
        public ICollection<ProjectDetails> ProjectDetails { get; private set; }
        public ICollection<Partner> Partners { get; private set; }

    }
}