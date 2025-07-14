using DesdeElBanquilloApplication.Services;
using DesdeElBanquilloApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DesdeElBanquilloApplication.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly ILigaService _service;

        public LeaguesController(ILigaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var leagues = await _service.GetAllAsync();
            return View(leagues);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var league = await _service.GetByIdAsync(id.Value);
            return league == null ? NotFound() : View(league);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["IdCountry"] = new SelectList(await _service.GetCountriesAsync(), "Value", "Text");
            return View(new LeagueViewModel
            {
                CreatedDate = DateTime.Now,
                IsActive = true
            });
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

            ViewData["IdCountry"] = new SelectList(await _service.GetCountriesAsync(), "Value", "Text", vm.IdCountry);
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var league = await _service.GetByIdAsync(id.Value);
            if (league == null) return NotFound();

            ViewData["IdCountry"] = new SelectList(await _service.GetCountriesAsync(), "Value", "Text", league.IdCountry);
            return View(league);
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

            ViewData["IdCountry"] = new SelectList(await _service.GetCountriesAsync(), "Value", "Text", vm.IdCountry);
            return View(vm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var league = await _service.GetByIdAsync(id.Value);
            return league == null ? NotFound() : View(league);
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