using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get() => await _context.Countries.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            var item = await _context.Countries.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country item)
        {
            _context.Countries.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.IdCountry }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Country item)
        {
            if (id != item.IdCountry) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Countries.Any(e => e.IdCountry == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Countries.FindAsync(id);
            if (item == null) return NotFound();
            _context.Countries.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
