using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using Microsoft.AspNetCore.Http;
using ComeTogether.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using ComeTogether.Service.Interfaces;
using System.Net.Http;
using System.Security.Claims;
using ComeTogether.Service;
using ComeTogether.Application.Common.Interfaces;

namespace WebMVC.Controllers
{
    public class ArticlesController : Controller
    {
       
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IArticleService _service;


        public ArticlesController(IArticleService service, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor/*, UserManager<ApplicationUser> userMgr*/)
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
       

        private async Task<bool> ArticleExists(int id)
        {
            return await _service.Exist(id);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,Name,Briefly,Description")] Article project, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                Article create = project;

                if (image != null && image.Length > 0)
                {
                    create.Photo = ImageConvertor.ConvertImageToBytes(image);
                }
                create.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

                _service.Add(create);
               
                ViewBag.Ok = "Article successfully saved";
                return View(create);
            }
            return View();
        }

        public async Task<ActionResult> GetArticle(int id)
        {

            // HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Articles/" + id.ToString()).Result;


            IEnumerable<UserProfileModel> stores =
                             from x in _context.Articles
                             where x.ArticleId == id
                             select new UserProfileModel
                             {
                                 Photo = x.Photo,
                                 Briefly = x.Briefly,
                                 DatePost = x.PostDate,

                                 ArticleId = x.ArticleId,
                                 ArticleName = x.Name,
                                 Description = x.Description,
                                 Category = x.Category.CategoryName,
                                 Comment = _context.Comments
                                       .Where(y => y.ArticleId == x.ArticleId)
                                       .Select(y => y.Content).ToList(),
                                 CommentDate = _context.Comments
                                       .Where(y => y.ArticleId == x.ArticleId)
                                       .Select(y => y.PostDate).ToList(),
                                 CommentedUserId = _context.Comments
                                       .Where(y => y.ArticleId == x.ArticleId)
                                       .Select(y => y.UserId).ToList(),
                                 CommentedUserName = _context.Comments
                                       .Where(y => y.ArticleId == x.ArticleId)
                                       .Select(y =>
                                       _context.ApplicationUsers.Where(x => x.Id == y.UserId)
                                       .Select(x => x.UserName).FirstOrDefault()).ToList(),



                             };
            Article article = _context.Articles.FirstOrDefault(x => x.ArticleId == id);
            return View(stores);
        }


        [Authorize]
        public async Task<ActionResult> AddComment(Comment model, int articleId)
        {

            var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();

            if (ModelState.IsValid)
            {
                Comment c = new Comment();

                DateTime d = DateTime.Now;


                c.ArticleId = articleId;
                c.Content = model.Content;
                c.PostDate = d;
                c.UserId = current;
                _context.Comments.Add(c);
                _context.SaveChanges();
                return RedirectToAction("GetArticle", new { id = articleId });
            }
            return RedirectToAction("GetArticle", new { id = articleId });

        }


        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            Article article = _context.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article != null)
            {
                return View(article);
            }
            else
            {
                return RedirectToAction("UserProfile", "Account");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(string name, string briefly, string description, int id)
        {

            var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();


            Article article = _context.Articles.Where(x => x.ArticleId == id).FirstOrDefault();
            if (article != null)
            {

                article.UserId = current;
                article.Briefly = briefly;
                article.Name = name;
                article.Description = description;



                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Articles/", id).Result;


                _context.SaveChanges();



            }
            else
            {
                ModelState.AddModelError("", "We can't find user");
            }
            return RedirectToAction("GetArticle", new { id = id });

        }



        public async Task<IActionResult> Index(string searchString)
        {

            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                var eventsSearch = _service.GetArticleByTitle(searchString);
                return View(eventsSearch);
            }
            var events = await _service.GetAll();
            // var events = _context.Articles.OrderBy(x=>x.Name).ToList();
            return View(events);

        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await Task.Run(() => _service.GetById(id));
            if (article == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Articles/" + id.ToString()).Result;

            return View(article);
        }


        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await Task.Run(() => _service.GetById(id));
            _service.Remove(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Articles/" + id.ToString()).Result;

            _context.Articles.Remove(article);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
