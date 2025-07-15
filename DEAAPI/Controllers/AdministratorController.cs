using Microsoft.AspNetCore.Mvc;
using DEAModels.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AdministratorController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> Get() => await _context.Administrators.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> Get(int id)
        {
            var item = await _context.Administrators.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Administrator>> Post(Administrator item)
        {
            _context.Administrators.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdAdministrator }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Administrator item)
        {
            if (id != item.IdAdministrator) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Administrators.Any(e => e.IdAdministrator == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Administrators.FindAsync(id);
            if (item == null) return NotFound();
            _context.Administrators.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
