using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.Extensions.Localization;

namespace WebMVC.Controllers
{
 
    public class DealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
     
        public DealsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor/*, UserManager<ApplicationUser> userMgr*/) 
        {
            _httpContextAccessor = httpContextAccessor;
       
            _context = context;
        }

     
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deals.ToListAsync());
        }

      
        public async Task<IActionResult> Details(int id)
        {
         
            var deal = await _context.Deals
                .FirstOrDefaultAsync(m => m.DealId == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.ProjectId = id;
            return View();
        }
       
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, IFormFile g)
        {
       
                Deal deal = new Deal();
                deal.PartnerId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
                 deal.ProjectId = id;

            if (_context.Deals.Where(x=>x.PartnerId == deal.PartnerId && x.ProjectId==deal.ProjectId).Any())
            {
                return RedirectToAction("Index", "Projects");
            }
                _context.Add(deal);
           
                await _context.SaveChangesAsync();
           
           
            return RedirectToAction("Index","Projects" );

        }
        [Authorize]
       
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }
            return View(deal);
        }

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Deal deal)
        {
            if (id != deal.DealId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealExists(deal.DealId))
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
            return View(deal);
        }
        [Authorize]
        // GET: Deals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deal = await _context.Deals
                .FirstOrDefaultAsync(m => m.DealId == id);
            if (deal == null)
            {
                return NotFound();
            }

            return View(deal);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var deal = await _context.Deals.FindAsync(id);
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealExists(int id)
        {
            return _context.Deals.Any(e => e.DealId == id);
        }
    }
}
