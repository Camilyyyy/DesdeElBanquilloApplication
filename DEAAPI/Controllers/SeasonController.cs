using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeasonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SeasonController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Season>>> Get() => await _context.Seasons.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Season>> Get(int id)
        {
            var item = await _context.Seasons.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Season>> Post(Season item)
        {
            _context.Seasons.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdSeason }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Season item)
        {
            if (id != item.IdSeason) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Seasons.Any(e => e.IdSeason == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Seasons.FindAsync(id);
            if (item == null) return NotFound();
            _context.Seasons.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
