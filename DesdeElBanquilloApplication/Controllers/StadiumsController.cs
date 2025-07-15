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
    public class StadiumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StadiumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stadiums
        public async Task<IActionResult> Index()
        {
            var desdeElBanquilloAppDBContext = _context.Stadiums.Include(s => s.Team);
            return View(await desdeElBanquilloAppDBContext.ToListAsync());
        }

        // GET: Stadiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.IdStadium == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // GET: Stadiums/Create
        public IActionResult Create()
        {
            ViewData["IdTeam"] = new SelectList(_context.Teams, "IdTeam", "Name");
            return View();
        }

        // POST: Stadiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStadium,Name,FoundedDate,Capacity,IdTeam")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stadium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", stadium.IdTeam);
            return View(stadium);
        }

        // GET: Stadiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums.FindAsync(id);
            if (stadium == null)
            {
                return NotFound();
            }
            ViewData["IdTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", stadium.IdTeam);
            return View(stadium);
        }

        // POST: Stadiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStadium,Name,FoundedDate,Capacity,IdTeam")] Stadium stadium)
        {
            if (id != stadium.IdStadium)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stadium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StadiumExists(stadium.IdStadium))
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
            ViewData["IdTeam"] = new SelectList(_context.Teams, "IdTeam", "Name", stadium.IdTeam);
            return View(stadium);
        }

        // GET: Stadiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stadium = await _context.Stadiums
                .Include(s => s.Team)
                .FirstOrDefaultAsync(m => m.IdStadium == id);
            if (stadium == null)
            {
                return NotFound();
            }

            return View(stadium);
        }

        // POST: Stadiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stadium = await _context.Stadiums.FindAsync(id);
            if (stadium != null)
            {
                _context.Stadiums.Remove(stadium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StadiumExists(int id)
        {
            return _context.Stadiums.Any(e => e.IdStadium == id);
        }
    }
}
