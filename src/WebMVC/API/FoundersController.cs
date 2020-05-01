//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ComeTogether.Domain.Entities;
//using ComeTogether.Infrastructure;

//namespace WebMVC.API
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FoundersController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public FoundersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Founders
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Founder>>> GetFounders()
//        {
//            return await _context.Founders.ToListAsync();
//        }

//        // GET: api/Founders/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Founder>> GetFounder(int id)
//        {
//            var founder = await _context.Founders.FindAsync(id);

//            if (founder == null)
//            {
//                return NotFound();
//            }

//            return founder;
//        }

//        // PUT: api/Founders/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutFounder(int id, Founder founder)
//        {
//            if (id != founder.FounderId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(founder).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FounderExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Founders
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPost]
//        public async Task<ActionResult<Founder>> PostFounder(Founder founder)
//        {
//            _context.Founders.Add(founder);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetFounder", new { id = founder.FounderId }, founder);
//        }

//        // DELETE: api/Founders/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Founder>> DeleteFounder(int id)
//        {
//            var founder = await _context.Founders.FindAsync(id);
//            if (founder == null)
//            {
//                return NotFound();
//            }

//            _context.Founders.Remove(founder);
//            await _context.SaveChangesAsync();

//            return founder;
//        }

//        private bool FounderExists(int id)
//        {
//            return _context.Founders.Any(e => e.FounderId == id);
//        }
//    }
//}
