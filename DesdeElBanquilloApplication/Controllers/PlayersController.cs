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
    public class PlayersController : Controller
    {
        private readonly PlayerApiService _playerService;
        private readonly CountryApiService _countryService;
        private readonly PositionApiService _positionService;
        private readonly TeamApiService _teamService;

        public PlayersController(PlayerApiService playerService, CountryApiService countryService, PositionApiService positionService, TeamApiService teamService)
        {
            _playerService = playerService;
            _countryService = countryService;
            _positionService = positionService;
            _teamService = teamService;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllAsync();
            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var player = await _playerService.GetByIdAsync(id.Value);
            if (player == null) return NotFound();
            return View(player);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name");
            ViewData["IdPosition"] = new SelectList(await _positionService.GetAllAsync(), "IdPosition", "Name");
            ViewData["IdTeam"] = new SelectList(await _teamService.GetAllAsync(), "IdTeam", "Name");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Player player)
        {
            if (ModelState.IsValid)
            {
                var ok = await _playerService.CreateAsync(player);
                if (ok) return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", player.IdCountry);
            ViewData["IdPosition"] = new SelectList(await _positionService.GetAllAsync(), "IdPosition", "Name", player.IdPosition);
            ViewData["IdTeam"] = new SelectList(await _teamService.GetAllAsync(), "IdTeam", "Name", player.IdTeam);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var player = await _playerService.GetByIdAsync(id.Value);
            if (player == null) return NotFound();
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", player.IdCountry);
            ViewData["IdPosition"] = new SelectList(await _positionService.GetAllAsync(), "IdPosition", "Name", player.IdPosition);
            ViewData["IdTeam"] = new SelectList(await _teamService.GetAllAsync(), "IdTeam", "Name", player.IdTeam);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Player player)
        {
            if (id != player.IdPlayer) return NotFound();
            if (ModelState.IsValid)
            {
                var ok = await _playerService.UpdateAsync(id, player);
                if (ok) return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", player.IdCountry);
            ViewData["IdPosition"] = new SelectList(await _positionService.GetAllAsync(), "IdPosition", "Name", player.IdPosition);
            ViewData["IdTeam"] = new SelectList(await _teamService.GetAllAsync(), "IdTeam", "Name", player.IdTeam);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var player = await _playerService.GetByIdAsync(id.Value);
            if (player == null) return NotFound();
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _playerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name", player.IdCountry);
            ViewData["IdPosition"] = new SelectList(_context.Position, "IdPosition", "Name", player.IdPosition);
            ViewData["IdTeam"] = new SelectList(_context.Team, "IdTeam", "Name", player.IdTeam);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.Country)
                .Include(p => p.Position)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.IdPlayer == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Player.Any(e => e.IdPlayer == id);
        }
    }
}
