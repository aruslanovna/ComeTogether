using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ComeTogether.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public byte[] Photo { get; set; }
       // public string Status { get; set; }
       // public string Experience { get; set; }
        public string WorkPlace { get; set; }
        public string Position { get; set; }
        public string Info { get; set; }
    }
}
