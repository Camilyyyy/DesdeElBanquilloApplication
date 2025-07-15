using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StadiumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StadiumController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stadium>>> Get() => await _context.Stadiums.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Stadium>> Get(int id)
        {
            var item = await _context.Stadiums.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Stadium>> Post(Stadium item)
        {
            _context.Stadiums.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdStadium }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Stadium item)
        {
            if (id != item.IdStadium) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Stadiums.Any(e => e.IdStadium == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Stadiums.FindAsync(id);
            if (item == null) return NotFound();
            _context.Stadiums.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
