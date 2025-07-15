using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesdeElBanquilloApplication.Models;
using DesdeElBanquilloApplication.Services;

namespace DesdeElBanquilloApplication.Controllers
{
    public class PositionsController : Controller
    {
        private readonly PositionApiService _service;

        public PositionsController(PositionApiService service)
        {
            _service = service;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var positions = await _service.GetAllAsync();
            return View(positions);
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var position = await _service.GetByIdAsync(id.Value);
            if (position == null) return NotFound();
            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                var ok = await _service.CreateAsync(position);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var position = await _service.GetByIdAsync(id.Value);
            if (position == null) return NotFound();
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Position position)
        {
            if (id != position.IdPosition) return NotFound();
            if (ModelState.IsValid)
            {
                var ok = await _service.UpdateAsync(id, position);
                if (ok) return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var position = await _service.GetByIdAsync(id.Value);
            if (position == null) return NotFound();
            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}