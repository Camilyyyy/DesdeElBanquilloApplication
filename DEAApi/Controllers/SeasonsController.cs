﻿using System;
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
    public class SeasonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SeasonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Seasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Season>>> GetSeasons()
        {
            return await _context.Seasons.Include(s => s.League).ToListAsync();
        }

        // GET: api/Seasons/5
        // EN: DEAApi/Controllers/SeasonsController.cs

        [HttpGet("{id}")]
        public async Task<ActionResult<Season>> GetSeason(int id)
        {
            // Usamos FirstOrDefaultAsync con Include para poder cargar la liga.
            var season = await _context.Seasons
                                       .Include(s => s.League)
                                       .FirstOrDefaultAsync(s => s.IdSeason == id);

            if (season == null)
            {
                return NotFound();
            }

            return season;
        }

        // PUT: api/Seasons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeason(int id, Season season)
        {
            if (id != season.IdSeason)
            {
                return BadRequest();
            }

            _context.Entry(season).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeasonExists(id))
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

        // POST: api/Seasons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Season>> PostSeason(Season season)
        {
            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeason", new { id = season.IdSeason }, season);
        }

        // DELETE: api/Seasons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
            {
                return NotFound();
            }

            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeasonExists(int id)
        {
            return _context.Seasons.Any(e => e.IdSeason == id);
        }
    }
}
