using ComeTogether.Application.Common.Interfaces;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using ComeTogether.Infrastructure.Repository;
using ComeTogether.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Encodings;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebMVC.Controllers;
using Xunit;

namespace xUnitTest
{
   public class ArticleRepositoryTest
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfArticles()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
         .UseInMemoryDatabase(databaseName: "Products Test")
         .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Articles.Add(
                    new Article { ArticleId = 12, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" }
               );

                context.Articles.Add(new Article { ArticleId = 13, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                context.Articles.Add(new Article { ArticleId = 14, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                context.Articles.Add(new Article { ArticleId = 15, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {

                Repository<Article> mockRep = new Repository<Article>(context);
                Task<IEnumerable<Article>> movies = mockRep.GetAll();

                Assert.Equal(4, movies.Result.Count());

            }
        }
    }
}
