using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FederationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FederationController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Federation>>> Get() => await _context.Federations.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Federation>> Get(int id)
        {
            var item = await _context.Federations.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Federation>> Post(Federation item)
        {
            _context.Federations.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdFederation }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Federation item)
        {
            if (id != item.IdFederation) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Federations.Any(e => e.IdFederation == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Federations.FindAsync(id);
            if (item == null) return NotFound();
            _context.Federations.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
