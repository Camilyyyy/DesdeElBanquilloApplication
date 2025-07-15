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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var desdeElBanquilloAppDBContext = _context.Matches.Include(m => m.AwayTeam).Include(m => m.Competition).Include(m => m.HomeTeam).Include(m => m.Stadium);
            return View(await desdeElBanquilloAppDBContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.Competition)
                .Include(m => m.HomeTeam)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.IdMatch == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["IdAwayTeam"] = new SelectList(_context.Teams, "IdTeam", "Name");
            ViewData["IdCompetition"] = new SelectList(_context.Competitions, "IdCompetition", "Name");
            ViewData["IdHomeTeam"] = new SelectList(_context.Teams, "IdTeam", "Name");
            ViewData["IdStadium"] = new SelectList(_context.Stadiums, "IdStadium", "Name");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMatch,MatchDate,HomeGoals,AwayGoals,Status,Referee,IdHomeTeam,IdAwayTeam,IdCompetition,IdStadium")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAwayTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdAwayTeam);
            ViewData["IdCompetition"] = new SelectList(_context.Competitions, "IdCompetition", "Name", match.IdCompetition);
            ViewData["IdHomeTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdHomeTeam);
            ViewData["IdStadium"] = new SelectList(_context.Stadiums, "IdStadium", "Name", match.IdStadium);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["IdAwayTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdAwayTeam);
            ViewData["IdCompetition"] = new SelectList(_context.Competitions, "IdCompetition", "Name", match.IdCompetition);
            ViewData["IdHomeTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdHomeTeam);
            ViewData["IdStadium"] = new SelectList(_context.Stadiums, "IdStadium", "Name", match.IdStadium);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatch,MatchDate,HomeGoals,AwayGoals,Status,Referee,IdHomeTeam,IdAwayTeam,IdCompetition,IdStadium")] Match match)
        {
            if (id != match.IdMatch)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.IdMatch))
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
            ViewData["IdAwayTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdAwayTeam);
            ViewData["IdCompetition"] = new SelectList(_context.Competitions, "IdCompetition", "Name", match.IdCompetition);
            ViewData["IdHomeTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", match.IdHomeTeam);
            ViewData["IdStadium"] = new SelectList(_context.Stadiums, "IdStadium", "Name", match.IdStadium);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.Competition)
                .Include(m => m.HomeTeam)
                .Include(m => m.Stadium)
                .FirstOrDefaultAsync(m => m.IdMatch == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.IdMatch == id);
        }
    }
}
