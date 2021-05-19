using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Deals = new HashSet<Deal>();
        }

        public byte[] Photo { get; set; }

        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }


        public string LastName { get; set; }


        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public ICollection<Deal> Deals{ get; set; }
       // public List<AuthenticationScheme> ExternalLogins { get; set; }
       // public string ReturnUrl { get; set; }
        // public List<int> foundedPartnerId { get; set; }
        // public List<int> joinedPartnerId { get; set; }


    }
}
