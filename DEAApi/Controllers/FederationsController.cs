using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEAApi.Data;
using DEAModels;

namespace DEAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FederationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FederationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Federations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Federation>>> GetFederations()
        {
            return await _context.Federations.Include(f => f.Country).ToListAsync();
        }

        // GET: api/Federations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Federation>> GetFederation(int id)
        {
            var federation = await _context.Federations.Include(f => f.Country).FirstOrDefaultAsync(f => f.IdFederation==id);

            if (federation == null)
            {
                return NotFound();
            }

            return federation;
        }

        // PUT: api/Federations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFederation(int id, Federation federation)
        {
            if (id != federation.IdFederation)
            {
                return BadRequest();
            }

            _context.Entry(federation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FederationExists(id))
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

        // POST: api/Federations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Federation>> PostFederation(Federation federation)
        {
            _context.Federations.Add(federation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFederation", new { id = federation.IdFederation }, federation);
        }

        // DELETE: api/Federations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFederation(int id)
        {
            var federation = await _context.Federations.FindAsync(id);
            if (federation == null)
            {
                return NotFound();
            }

            _context.Federations.Remove(federation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FederationExists(int id)
        {
            return _context.Federations.Any(e => e.IdFederation == id);
        }
    }
}
