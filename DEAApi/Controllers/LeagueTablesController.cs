using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEAApi.Data;
using DEAModels;

namespace DEAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueTablesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeagueTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LeagueTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueTable>>> GetLeagueTables()
        {
            return await _context.LeagueTables.ToListAsync();
        }

        // GET: api/LeagueTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueTable>> GetLeagueTable(int id)
        {
            var leagueTable = await _context.LeagueTables.FindAsync(id);

            if (leagueTable == null)
            {
                return NotFound();
            }

            return leagueTable;
        }

        // PUT: api/LeagueTables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeagueTable(int id, LeagueTable leagueTable)
        {
            if (id != leagueTable.IdLeagueTable)
            {
                return BadRequest();
            }

            _context.Entry(leagueTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeagueTableExists(id))
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

        // POST: api/LeagueTables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeagueTable>> PostLeagueTable(LeagueTable leagueTable)
        {
            _context.LeagueTables.Add(leagueTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeagueTable", new { id = leagueTable.IdLeagueTable }, leagueTable);
        }

        // DELETE: api/LeagueTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeagueTable(int id)
        {
            var leagueTable = await _context.LeagueTables.FindAsync(id);
            if (leagueTable == null)
            {
                return NotFound();
            }

            _context.LeagueTables.Remove(leagueTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeagueTableExists(int id)
        {
            return _context.LeagueTables.Any(e => e.IdLeagueTable == id);
        }
    }
}
