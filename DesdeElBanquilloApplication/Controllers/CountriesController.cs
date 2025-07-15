using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using DesdeElBanquilloApplication.Services;

namespace DesdeElBanquilloApplication.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountryApiService _service;

        public CountriesController(CountryApiService service)
        {
            _service = service;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var countries = await _service.GetAllAsync();
            return View(countries);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var country = await _service.GetByIdAsync(id.Value);
            if (country == null) return NotFound();
            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                var ok = await _service.CreateAsync(country);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var country = await _service.GetByIdAsync(id.Value);
            if (country == null) return NotFound();
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.IdCountry) return NotFound();
            if (ModelState.IsValid)
            {
                var ok = await _service.UpdateAsync(id, country);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var country = await _service.GetByIdAsync(id.Value);
            if (country == null) return NotFound();
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}