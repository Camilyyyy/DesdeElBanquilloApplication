using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LeagueController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> Get() => await _context.Leagues.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<League>> Get(int id)
        {
            var item = await _context.Leagues.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<League>> Post(League item)
        {
            _context.Leagues.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdLeague }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, League item)
        {
            if (id != item.IdLeague) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Leagues.Any(e => e.IdLeague == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Leagues.FindAsync(id);
            if (item == null) return NotFound();
            _context.Leagues.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
