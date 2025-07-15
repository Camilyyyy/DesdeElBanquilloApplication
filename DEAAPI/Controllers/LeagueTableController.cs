using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueTableController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LeagueTableController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueTable>>> Get() => await _context.LeagueTables.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueTable>> Get(int id)
        {
            var item = await _context.LeagueTables.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<LeagueTable>> Post(LeagueTable item)
        {
            _context.LeagueTables.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdLeagueTable }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LeagueTable item)
        {
            if (id != item.IdLeagueTable) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.LeagueTables.Any(e => e.IdLeagueTable == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.LeagueTables.FindAsync(id);
            if (item == null) return NotFound();
            _context.LeagueTables.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
