using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeamController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get() => await _context.Teams.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> Get(int id)
        {
            var item = await _context.Teams.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> Post(Team item)
        {
            _context.Teams.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdTeam }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Team item)
        {
            if (id != item.IdTeam) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teams.Any(e => e.IdTeam == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Teams.FindAsync(id);
            if (item == null) return NotFound();
            _context.Teams.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
