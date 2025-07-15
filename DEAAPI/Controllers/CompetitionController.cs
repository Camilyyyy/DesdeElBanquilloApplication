using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CompetitionController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> Get() => await _context.Competitions.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> Get(int id)
        {
            var item = await _context.Competitions.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Competition>> Post(Competition item)
        {
            _context.Competitions.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdCompetition }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Competition item)
        {
            if (id != item.IdCompetition) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Competitions.Any(e => e.IdCompetition == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Competitions.FindAsync(id);
            if (item == null) return NotFound();
            _context.Competitions.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
