using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;

namespace WebMVC.Controllers
{
    public static class FillDB
    {
    
            public static void Initialize(ApplicationDbContext context)
            {
                if (!context.Articles.Any())
                {
                    context.Articles.AddRange(
                        new Article
                        {
                           Name = "Test",
                           Description = "Test description",
                           Briefly="Briefly"
                        },
                        new Article
                        {
                            Name = "Test1",
                            Description = "Test description1",
                            Briefly = "Briefly1"
                        }
                    );
                    context.SaveChanges();
                }
            }
        
    }
}
