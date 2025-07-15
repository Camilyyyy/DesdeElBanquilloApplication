using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PlayerController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> Get() => await _context.Players.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> Get(int id)
        {
            var item = await _context.Players.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Post(Player item)
        {
            _context.Players.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdPlayer }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Player item)
        {
            if (id != item.IdPlayer) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Players.Any(e => e.IdPlayer == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Players.FindAsync(id);
            if (item == null) return NotFound();
            _context.Players.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
