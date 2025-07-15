using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DEAModels;
using DEAApi.Data;

namespace DesdeElBanquilloApplication.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var desdeElBanquilloAppDBContext = _context.Competitions.Include(c => c.Country).Include(c => c.Federation).Include(c => c.Season);
            return View(await desdeElBanquilloAppDBContext.ToListAsync());
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.Country)
                .Include(c => c.Federation)
                .Include(c => c.Season)
                .FirstOrDefaultAsync(m => m.IdCompetition == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "Name");
            ViewData["IdFederation"] = new SelectList(_context.Federations, "IdFederation", "Name");
            ViewData["IdSeason"] = new SelectList(_context.Seasons, "IdSeason", "Name");
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompetition,Name,IdCountry,IdSeason,IdFederation")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "Name", competition.IdCountry);
            ViewData["IdFederation"] = new SelectList(_context.Federations, "IdFederation", "Name", competition.IdFederation);
            ViewData["IdSeason"] = new SelectList(_context.Seasons, "IdSeason", "Name", competition.IdSeason);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "Name", competition.IdCountry);
            ViewData["IdFederation"] = new SelectList(_context.Federations, "IdFederation", "Name", competition.IdFederation);
            ViewData["IdSeason"] = new SelectList(_context.Seasons, "IdSeason", "Name", competition.IdSeason);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompetition,Name,IdCountry,IdSeason,IdFederation")] Competition competition)
        {
            if (id != competition.IdCompetition)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.IdCompetition))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(_context.Countries, "IdCountry", "Name", competition.IdCountry);
            ViewData["IdFederation"] = new SelectList(_context.Federations, "IdFederation", "Name", competition.IdFederation);
            ViewData["IdSeason"] = new SelectList(_context.Seasons, "IdSeason", "Name", competition.IdSeason);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competitions
                .Include(c => c.Country)
                .Include(c => c.Federation)
                .Include(c => c.Season)
                .FirstOrDefaultAsync(m => m.IdCompetition == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition != null)
            {
                _context.Competitions.Remove(competition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.IdCompetition == id);
        }
    }
}
