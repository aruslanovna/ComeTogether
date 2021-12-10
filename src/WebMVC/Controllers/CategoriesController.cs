using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using ComeTogether.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

       
        public CategoriesController(ApplicationDbContext context)
        {
           

            _context = context;
        }


        // GET: Categories
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                var categor = _context.Categories.Where(x => x.CategoryName.Contains(searchString)).ToList();
                return View(categor);
            }
           
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

           Category category = _context.Categories.Where(x=> x.CategoryId==id).FirstOrDefault();
            //.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] Category category, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)

                {
                    category.Picture= ImageConvertor.ConvertImageToBytes(image);
                }
                _context.Categories.Add(category);
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {



            if (id == null)
            {
                return NotFound();
            }
            Category category = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Categories.Update(category);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            Category category = _context.Categories.Where(x => x.CategoryId == id).FirstOrDefault();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




       

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(x => x.CategoryId == id);
        }
    }
}
