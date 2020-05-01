using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure;

namespace WebMVC.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Deals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deal>>> GetDeals()
        {
            return await _context.Deals.ToListAsync();
        }

        // GET: api/Deals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Deal>> GetDeal(string id)
        {
            var deal = await _context.Deals.FindAsync(id);

            if (deal == null)
            {
                return NotFound();
            }

            return deal;
        }

        // PUT: api/Deals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeal(int id, Deal deal)
        {
            if (id != deal.DealId)
            {
                return BadRequest();
            }

            _context.Entry(deal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Deal>> PostDeal(Deal deal)
        {
            _context.Deals.Add(deal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DealExists(deal.DealId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDeal", new { id = deal.DealId }, deal);
        }

        // DELETE: api/Deals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Deal>> DeleteDeal(string id)
        {
            var deal = await _context.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }

            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();

            return deal;
        }

        private bool DealExists(int id)
        {
            return _context.Deals.Any(e => e.DealId == id);
        }
    }
}
