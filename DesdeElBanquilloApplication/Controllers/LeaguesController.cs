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
    public class LeaguesController : Controller
    {
        private readonly LeagueApiService _service;
        private readonly CountryApiService _countryService;

        public LeaguesController(LeagueApiService service, CountryApiService countryService)
        {
            _service = service;
            _countryService = countryService;
        }

        // GET: Leagues
        public async Task<IActionResult> Index()
        {
            var leagues = await _service.GetAllAsync();
            return View(leagues);
        }

        // GET: Leagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var league = await _service.GetByIdAsync(id.Value);
            if (league == null) return NotFound();
            return View(league);
        }

        // GET: Leagues/Create
        public async Task<IActionResult> Create()
        {
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name");
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(League league)
        {
            if (ModelState.IsValid)
            {
                var ok = await _service.CreateAsync(league);
                if (ok) return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", league.IdCountry);
            return View(league);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var league = await _service.GetByIdAsync(id.Value);
            if (league == null) return NotFound();
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", league.IdCountry);
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, League league)
        {
            if (id != league.IdLeague) return NotFound();
            if (ModelState.IsValid)
            {
                var ok = await _service.UpdateAsync(id, league);
                if (ok) return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(await _countryService.GetAllAsync(), "IdCountry", "Name", league.IdCountry);
            return View(league);
        }

        // GET: Leagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var league = await _service.GetByIdAsync(id.Value);
            if (league == null) return NotFound();
            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}