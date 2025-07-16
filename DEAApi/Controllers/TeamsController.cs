// EN: DEAApi/Controllers/TeamsController.cs

using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// Asegúrate de que este using apunte a la carpeta donde está tu DbContext de la API
using DEAApi.Data;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ApplicationDbContext _context; // REEMPLAZA 'YourApiDbContext' POR EL NOMBRE DE TU DBContext

    public TeamsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Teams
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
    {
        // Se incluyen las entidades relacionadas para que el JSON de respuesta contenga sus datos
        return await _context.Teams
                             .Include(t => t.Competition)
                             .Include(t => t.Country)
                             .Include(t => t.League)
                             .ToListAsync();
    }

    // GET: api/Teams/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Team>> GetTeam(int id)
    {
        var team = await _context.Teams
                                 .Include(t => t.Competition)
                                 .Include(t => t.Country)
                                 .Include(t => t.League)
                                 .FirstOrDefaultAsync(t => t.IdTeam == id);

        if (team == null)
        {
            return NotFound();
        }

        return team;
    }

    // PUT: api/Teams/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeam(int id, Team team)
    {
        if (id != team.IdTeam)
        {
            return BadRequest();
        }

        _context.Entry(team).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TeamExists(id))
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

    // POST: api/Teams
    [HttpPost]
    public async Task<ActionResult<Team>> PostTeam(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTeam", new { id = team.IdTeam }, team);
    }

    // DELETE: api/Teams/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null)
        {
            return NotFound();
        }

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TeamExists(int id)
    {
        return _context.Teams.Any(e => e.IdTeam == id);
    }
}