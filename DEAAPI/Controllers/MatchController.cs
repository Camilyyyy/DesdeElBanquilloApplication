using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MatchController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> Get() => await _context.Matches.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> Get(int id)
        {
            var item = await _context.Matches.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Match>> Post(Match item)
        {
            _context.Matches.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdMatch }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Match item)
        {
            if (id != item.IdMatch) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Matches.Any(e => e.IdMatch == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Matches.FindAsync(id);
            if (item == null) return NotFound();
            _context.Matches.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
