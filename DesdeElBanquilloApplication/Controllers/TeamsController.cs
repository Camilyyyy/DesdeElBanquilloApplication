using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesdeElBanquilloApplication.Models;

namespace DesdeElBanquilloApplication.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DesdeElBanquilloAppDBContext _context;

        public TeamsController(DesdeElBanquilloAppDBContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var desdeElBanquilloAppDBContext = _context.Team.Include(t => t.Competition).Include(t => t.Country).Include(t => t.League).Include(t => t.Stadium);
            return View(await desdeElBanquilloAppDBContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .Include(t => t.Competition)
                .Include(t => t.Country)
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync(m => m.IdTeam == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Set<Competition>(), "IdCompetition", "Name");
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "IdCountry", "Name");
            ViewData["LeagueId"] = new SelectList(_context.Set<League>(), "IdLeague", "Name");
            ViewData["StadiumId"] = new SelectList(_context.Set<Stadium>(), "IdStadium", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTeam,Name,City,FoundedDate,CompetitionId,CountryId,StadiumId,LeagueId")] Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Set<Competition>(), "IdCompetition", "Name", team.CompetitionId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "IdCountry", "Name", team.CountryId);
            ViewData["LeagueId"] = new SelectList(_context.Set<League>(), "IdLeague", "Name", team.LeagueId);
            ViewData["StadiumId"] = new SelectList(_context.Set<Stadium>(), "IdStadium", "Name", team.StadiumId);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Set<Competition>(), "IdCompetition", "Name", team.CompetitionId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "IdCountry", "Name", team.CountryId);
            ViewData["LeagueId"] = new SelectList(_context.Set<League>(), "IdLeague", "Name", team.LeagueId);
            ViewData["StadiumId"] = new SelectList(_context.Set<Stadium>(), "IdStadium", "Name", team.StadiumId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTeam,Name,City,FoundedDate,CompetitionId,CountryId,StadiumId,LeagueId")] Team team)
        {
            if (id != team.IdTeam)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.IdTeam))
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
            ViewData["CompetitionId"] = new SelectList(_context.Set<Competition>(), "IdCompetition", "Name", team.CompetitionId);
            ViewData["CountryId"] = new SelectList(_context.Set<Country>(), "IdCountry", "Name", team.CountryId);
            ViewData["LeagueId"] = new SelectList(_context.Set<League>(), "IdLeague", "Name", team.LeagueId);
            ViewData["StadiumId"] = new SelectList(_context.Set<Stadium>(), "IdStadium", "Name", team.StadiumId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .Include(t => t.Competition)
                .Include(t => t.Country)
                .Include(t => t.League)
                .Include(t => t.Stadium)
                .FirstOrDefaultAsync(m => m.IdTeam == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Team.Any(e => e.IdTeam == id);
        }
    }
}
