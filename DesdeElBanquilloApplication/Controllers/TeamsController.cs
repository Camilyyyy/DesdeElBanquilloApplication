using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesdeElBanquilloApplication.Models;
using DesdeElBanquilloApplication.Services;

namespace DesdeElBanquilloApplication.Controllers
{
    public class TeamsController : Controller
    {
        private readonly TeamApiService _service;

        public TeamsController(TeamApiService service)
        {
            _service = service;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _service.GetAllAsync();
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var team = await _service.GetByIdAsync(id.Value);
            if (team == null) return NotFound();
            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            // Si necesitas combos, crea servicios para Competition, Country, League y consúmelos aquí
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team)
        {
            if (ModelState.IsValid)
            {
                var ok = await _service.CreateAsync(team);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var team = await _service.GetByIdAsync(id.Value);
            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.IdTeam) return NotFound();
            if (ModelState.IsValid)
            {
                var ok = await _service.UpdateAsync(id, team);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var team = await _service.GetByIdAsync(id.Value);
            if (team == null) return NotFound();
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
}
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTeam,Name,City,FoundedDate,IdCompetition,IdCountry,IdLeague")] Team team)
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
            ViewData["IdCompetition"] = new SelectList(_context.Competition, "IdCompetition", "Name", team.IdCompetition);
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name", team.IdCountry);
            ViewData["IdLeague"] = new SelectList(_context.League, "IdLeague", "Name", team.IdLeague);
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
