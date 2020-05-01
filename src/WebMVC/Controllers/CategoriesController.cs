using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;

using ComeTogether.Infrastructure.Persistence;
using ComeTogether.Infrastructure;
using ComeTogether.Service.Interfaces;

namespace WebMVC.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext _context;
        ICategoryService _unitOfWork;
        public CategoriesController(ApplicationDbContext context, ICategoryService unitOfWork)
        {
             _unitOfWork = unitOfWork;
             _context = context;
        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.GetById(id);
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
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] Category category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Add(category);
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
            Category category = _unitOfWork.GetById(id);
            if (category == null)
                return NotFound();
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

            var category = _unitOfWork.GetById(id);
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
            _unitOfWork.Remove(id);
           await _context.SaveChangesAsync();
          
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(x=>x.CategoryId==id);
        }
    }
}
