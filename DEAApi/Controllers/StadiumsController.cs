// EN: DEAApi/Controllers/StadiumsController.cs

using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// Asegúrate de que este using apunte a la carpeta donde está tu DbContext de la API
using DEAApi.Data;

[Route("api/[controller]")]
[ApiController]
public class StadiumsController : ControllerBase
{
    private readonly ApplicationDbContext _context; // REEMPLAZA 'YourApiDbContext' POR EL NOMBRE DE TU DBContext

    public StadiumsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Stadiums
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Stadium>>> GetStadiums()
    {
        // Se añade .Include() para que el JSON de respuesta contenga los datos del equipo.
        return await _context.Stadiums.Include(s => s.Team).ToListAsync();
    }

    // GET: api/Stadiums/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Stadium>> GetStadium(int id)
    {
        // Se usa FirstOrDefaultAsync con .Include() para obtener el equipo relacionado.
        var stadium = await _context.Stadiums
                                    .Include(s => s.Team)
                                    .FirstOrDefaultAsync(s => s.IdStadium == id);

        if (stadium == null)
        {
            return NotFound();
        }

        return stadium;
    }

    // PUT: api/Stadiums/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStadium(int id, Stadium stadium)
    {
        if (id != stadium.IdStadium)
        {
            return BadRequest();
        }

        _context.Entry(stadium).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StadiumExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Stadiums
    [HttpPost]
    public async Task<ActionResult<Stadium>> PostStadium(Stadium stadium)
    {
        _context.Stadiums.Add(stadium);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStadium", new { id = stadium.IdStadium }, stadium);
    }

    // DELETE: api/Stadiums/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStadium(int id)
    {
        var stadium = await _context.Stadiums.FindAsync(id);
        if (stadium == null)
        {
            return NotFound();
        }

        _context.Stadiums.Remove(stadium);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StadiumExists(int id)
    {
        return _context.Stadiums.Any(e => e.IdStadium == id);
    }
}