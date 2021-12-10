using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class Sponsor
    {

        public DateTime Date { get; set; }
        public Sponsor()
        {
            Projects = new HashSet<Project>();
        }

        public int SponsorId { get; set; }

        public int Charity { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Project> Projects { get; private set; }
    }

}



