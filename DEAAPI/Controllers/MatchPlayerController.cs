using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchPlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MatchPlayerController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchPlayer>>> Get() => await _context.MatchPlayers.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchPlayer>> Get(int id)
        {
            var item = await _context.MatchPlayers.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<MatchPlayer>> Post(MatchPlayer item)
        {
            _context.MatchPlayers.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdMatchPlayers }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MatchPlayer item)
        {
            if (id != item.IdMatchPlayers) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.MatchPlayers.Any(e => e.IdMatchPlayers == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.MatchPlayers.FindAsync(id);
            if (item == null) return NotFound();
            _context.MatchPlayers.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
