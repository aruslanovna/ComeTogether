using System;
using System.Collections.Generic;
using System.Text;
using ComeTogether.Domain.Entities;
using Microsoft.AspNetCore.Identity;
    
namespace ComeTogether.Infrastructure.Identity
{
   
  public class RoleEdit
        {
            public IdentityRole Role { get; set; }
            public IEnumerable<ApplicationUser> Members { get; set; }
            public IEnumerable<ApplicationUser> NonMembers { get; set; }
        }
    }
