using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class Client
    {


        public Client()
        {
            Projects = new HashSet<Project>();
        }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }

        public int Bill { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Project> Projects { get; private set; }
    }

}



