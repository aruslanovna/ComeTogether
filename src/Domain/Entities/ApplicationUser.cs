using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComeTogether.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public byte[] Photo { get; set; }
        // public string Status { get; set; }
        // public string Experience { get; set; }
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }
        //public int foundedPartnerId { get; set; }
        //public int joinedPartnerId { get; set; }
   //     CONSTRAINT[FK_AspNetUsers_ToTable] FOREIGN KEY([FoundedPartnerId]) REFERENCES[dbo].[Projects] ([ProjectId]),
   //CONSTRAINT[FK_AspNetUsers_ToTable_1] FOREIGN KEY([JoinedPartnerId]) REFERENCES[dbo].[Projects] ([ProjectId])
       public string LastName { get; set; }
       
       
        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
    }
}
