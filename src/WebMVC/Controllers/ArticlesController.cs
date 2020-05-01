using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using Microsoft.AspNetCore.Http;
using ComeTogether.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;

using System.Diagnostics;
using ComeTogether.Service.Interfaces;

namespace WebMVC.Controllers
{
    public class ArticlesController : Controller
    {
       

       
            private readonly ApplicationDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;
             private readonly IArticleService _unitOfWork;
        public ArticlesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IArticleService unitOfWork/*, UserManager<ApplicationUser> userMgr*/)
            {
             _unitOfWork= unitOfWork;
                 _httpContextAccessor = httpContextAccessor;
                _context = context;
            }

           
            private bool ArticleExists(int id)
            {
                return _context.Articles.Any(e => e.ArticleId == id);
            }
        [Authorize]
        public ActionResult Create(Article model)
            {
                var current = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value.ToString();
                Article c = new Article();


                if (ModelState.IsValid)
                {

                    //var file = Request.Files["ImageData"];
                    //if (file.InputStream != null)
                    //{

                    //    byte[] imageBytes = null;
                    //    System.IO.BinaryReader reader = new System.IO.BinaryReader(file.InputStream);
                    //    imageBytes = reader.ReadBytes((int)file.ContentLength);


                    //    c.Photo = imageBytes;
                    //}

                    c.UserId = current;
                    c.Name = model.Name;
                    if (String.IsNullOrEmpty(c.Name))
                        ModelState.AddModelError("Name", "Resources.Content.TitleRequired");
                    c.Briefly = model.Briefly;

                    c.Description = model.Description;

                    _context.Articles.Add(c);
                    _context.SaveChanges();
                    ViewBag.Ok = "Resources.Content.ArticleSaved";
                    return View(c);
                }
                return View();
              

            }



          

            public ActionResult GetArticle(int id)
            {

               
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
                                       _context.ApplicationUsers.Where(x=>x.Id==y.UserId)
                                       .Select(x=>x.UserName).FirstOrDefault()).ToList(),



                             };
                Article article = _context.Articles.FirstOrDefault(x => x.ArticleId == id);
                return View(stores);
            }
           

            [Authorize]
            public ActionResult AddComment(Comment model, int articleId)
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
            public ActionResult Edit(int id)
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




                   
                        _context.SaveChanges();
                    
                 

                }
                else
                {
                    ModelState.AddModelError("", "We can't find user");
                }
                return RedirectToAction("GetArticle", new { id = id });

            }


  

    // GET: Articles
    public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

       
        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.ArticleId == id);
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
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
