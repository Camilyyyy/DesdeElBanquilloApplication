// EN: DesdeElBanquilloApplication/Controllers/StadiumsController.cs

using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class StadiumsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public StadiumsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar Dropdown de Equipos ---
        private async Task PopulateTeamsDropdownAsync(object? selectedTeam = null)
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/teams");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<List<Team>>(json, _jsonOptions);
                ViewData["IdTeam"] = new SelectList(teams, "IdTeam", "Name", selectedTeam);
            }
        }

        // GET: Stadiums
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/stadiums");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Stadium>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var stadiums = JsonSerializer.Deserialize<List<Stadium>>(jsonString, _jsonOptions);
            return View(stadiums);
        }

        // GET: Stadiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var client = GetApiClient();
            var response = await client.GetAsync($"api/stadiums/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var jsonString = await response.Content.ReadAsStringAsync();
            var stadium = JsonSerializer.Deserialize<Stadium>(jsonString, _jsonOptions);
            return View(stadium);
        }

        // GET: Stadiums/Create
        public async Task<IActionResult> Create()
        {
            await PopulateTeamsDropdownAsync();
            return View();
        }

        // POST: Stadiums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FoundedDate,Capacity,IdTeam")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(stadium), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/stadiums", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el estadio desde la API.");
            }
            await PopulateTeamsDropdownAsync(stadium.IdTeam);
            return View(stadium);
        }

        // GET: Stadiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var client = GetApiClient();
            var response = await client.GetAsync($"api/stadiums/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var jsonString = await response.Content.ReadAsStringAsync();
            var stadium = JsonSerializer.Deserialize<Stadium>(jsonString, _jsonOptions);
            if (stadium == null) return NotFound();
            await PopulateTeamsDropdownAsync(stadium.IdTeam);
            return View(stadium);
        }

        // POST: Stadiums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStadium,Name,FoundedDate,Capacity,IdTeam")] Stadium stadium)
        {
            if (id != stadium.IdStadium) return NotFound();
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(stadium), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/stadiums/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el estadio desde la API.");
            }
            await PopulateTeamsDropdownAsync(stadium.IdTeam);
            return View(stadium);
        }

        // GET: Stadiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id);
        }

        // POST: Stadiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/stadiums/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}