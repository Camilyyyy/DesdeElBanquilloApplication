using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> Get() => await _context.Positions.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> Get(int id)
        {
            var item = await _context.Positions.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Position>> Post(Position item)
        {
            _context.Positions.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdPosition }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Position item)
        {
            if (id != item.IdPosition) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Positions.Any(e => e.IdPosition == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Positions.FindAsync(id);
            if (item == null) return NotFound();
            _context.Positions.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
