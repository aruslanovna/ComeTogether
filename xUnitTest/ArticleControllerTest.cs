using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using ComeTogether.Infrastructure.Interface;
using ComeTogether.Service.Interfaces;
using ComeTogether.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Controllers;
using Xunit;

namespace xUnitTest
{
    public class ArticleControllerTest
    {
        DbContextOptions<ApplicationDbContext> options;
        public ArticleControllerTest()
        {
            options = Instantiate();
        }

        public DbContextOptions<ApplicationDbContext> Instantiate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
      .UseInMemoryDatabase(databaseName: "Products Test")
      .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // context.Articles.Add(
                //     new Article { ArticleId = 16, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" }
                //);

                // context.Articles.Add(new Article { ArticleId = 17, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                // context.Articles.Add(new Article { ArticleId = 18, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                // context.Articles.Add(new Article { ArticleId = 19, Name = "dsfds", UserId = "dsf", Briefly = "fsdfsf" });
                // context.SaveChanges();
            }
            return options;
        }

        [Fact]
        public void IndexReturnsAViewResultWithAListOfArticles()
        {
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(u => u.ArticlesRepository.GetAll()).Returns(GetTestUsersAsync());
            ArticleService articleService = new ArticleService(mockUnit.Object);
            var mock = new Mock<IArticleService>();
            mock.Setup(u => u.GetAll()).Returns(GetTestUsersAsync());

            Assert.Equal(articleService.GetAll().Result.Count(), mock.Object.GetAll().Result.Count());
        }


        [Fact]
        public async Task IndexReturnsAViewResultWithAListOfArticlesByTitleZero()
        {
            string search = "aa";
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(u => u.ArticlesRepository);
            var mockServ = new Mock<IArticleService>();
            mockServ.Setup(u => u.GetArticleByTitle(It.IsAny<string>())).Returns(GetTestArticlesByTitle(search));
            
            using (var context = new ApplicationDbContext(options))
            {
                var http = new Mock<IHttpContextAccessor>();
                var controller = new ArticlesController(mockServ.Object, context, http.Object);

                // Act
                var result = controller.Index(search);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<Article>>(viewResult.Model);
                Assert.Equal(GetTestArticlesByTitle(search).Count(), model.Count());
            }
        }


        
    


    [Fact]
        public async Task IndexReturnsAViewResultWithAListOfArticlesByTitle()
        {
            string search = "ok";
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(u => u.ArticlesRepository);
            var mockServ = new Mock<IArticleService>();
            mockServ.Setup(u => u.GetArticleByTitle(It.IsAny<string>())).Returns(GetTestArticlesByTitle(search));
           
            using (var context = new ApplicationDbContext(options))
            {
                var http = new Mock<IHttpContextAccessor>();
                var controller = new ArticlesController(mockServ.Object, context, http.Object);

                // Act
                var result = controller.Index(search);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result.Result);
                var model = Assert.IsAssignableFrom<IEnumerable<Article>>(viewResult.Model);
                Assert.Equal(GetTestArticlesByTitle(search).Count(), model.Count());
            }
        }


        public bool EqualArt(Article o1, Article o2)
        {

            return Equals(o2.ArticleId, o1.ArticleId) && Equals(o2.Name, o1.Name);

            return false;
        }

        [Fact]
        public void ReturnsArticleByIDNotFound()
        {
            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(u => u.ArticlesRepository.GetById(It.IsAny<int>())).Returns(new Article());
            ArticleService articleService = new ArticleService(mockUnit.Object);



            using (var context = new ApplicationDbContext(options))
            {
                var http = new Mock<IHttpContextAccessor>();
                var controller = new ArticlesController(articleService, context, http.Object);
                // Act
                var result = controller.Details(null);
                // Assert

                var viewResult = Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        private IEnumerable<Article> GetTestArticles()
        {
            var articles = new List<Article>
            {
                new Article { ArticleId=12, Name="aadsfds", UserId="dsf", Briefly="fsdfsf" },
               new Article { ArticleId=13, Name="dsfds", UserId="dsf", Briefly="fsdfsf" },
               new Article { ArticleId=14, Name="aadsfds", UserId="dsf", Briefly="fsdfsf" },
               new Article { ArticleId=15, Name="dsfds", UserId="dsf", Briefly="fsdfsf" }

            };
            return articles;
        }
        private async Task<IEnumerable<Article>> GetTestUsersAsync()
        {
            var articles = await Task.Run(() => GetTestArticles());

            return articles;
        }
        private IEnumerable<Article> GetTestArticlesByTitle(string search)
        {
            var articles= GetTestArticles().Where(x => x.Name.StartsWith(search)).ToList();
            return articles;
        }
    }
}

