using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DesdeElBanquilloApplication.Data;
using DesdeElBanquilloApplication.Services;
using DesdeElBanquilloApplication.ViewModels;

namespace DesdeElBanquilloApplication.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly ILigaService _service;
        private readonly DesdeElBanquilloAppDBContext _context;

        public LeaguesController(ILigaService service,
                                 DesdeElBanquilloAppDBContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listVm = await _service.GetAllAsync();
            return View(listVm);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var vm = await _service.GetByIdAsync(id.Value);
            return vm == null ? NotFound() : View(vm);
        }

        public IActionResult Create()
        {
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name");
            return View(new LeagueViewModel { CreatedDate = DateTime.Now, IsActive = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeagueViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name", vm.IdCountry);
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var vm = await _service.GetByIdAsync(id.Value);
            if (vm == null) return NotFound();
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name", vm.IdCountry);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeagueViewModel vm)
        {
            if (id != vm.IdLeague) return NotFound();
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCountry"] = new SelectList(_context.Country, "IdCountry", "Name", vm.IdCountry);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var vm = await _service.GetByIdAsync(id.Value);
            return vm == null ? NotFound() : View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}