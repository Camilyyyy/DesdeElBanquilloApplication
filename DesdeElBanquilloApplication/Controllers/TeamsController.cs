// EN: DesdeElBanquilloApplication/Controllers/TeamsController.cs

using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public TeamsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        private async Task PopulateDropdownsAsync(Team? team = null)
        {
            var client = GetApiClient();

            var competitionsResponse = await client.GetAsync("api/competitions");
            if (competitionsResponse.IsSuccessStatusCode)
            {
                var json = await competitionsResponse.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<Competition>>(json, _jsonOptions);
                ViewData["IdCompetition"] = new SelectList(items, "IdCompetition", "Name", team?.IdCompetition);
            }

            var countriesResponse = await client.GetAsync("api/countries");
            if (countriesResponse.IsSuccessStatusCode)
            {
                var json = await countriesResponse.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<Country>>(json, _jsonOptions);
                ViewData["IdCountry"] = new SelectList(items, "IdCountry", "Name", team?.IdCountry);
            }

            var leaguesResponse = await client.GetAsync("api/leagues");
            if (leaguesResponse.IsSuccessStatusCode)
            {
                var json = await leaguesResponse.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<League>>(json, _jsonOptions);
                ViewData["IdLeague"] = new SelectList(items, "IdLeague", "Name", team?.IdLeague);
            }
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/teams");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Team>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var teams = JsonSerializer.Deserialize<List<Team>>(jsonString, _jsonOptions);
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var client = GetApiClient();
            var response = await client.GetAsync($"api/teams/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var jsonString = await response.Content.ReadAsStringAsync();
            var team = JsonSerializer.Deserialize<Team>(jsonString, _jsonOptions);
            return View(team);
        }

        // GET: Teams/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,FoundedDate,IdCompetition,IdCountry,IdLeague")] Team team)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(team), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/teams", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el equipo desde la API.");
            }
            await PopulateDropdownsAsync(team);
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var client = GetApiClient();
            var response = await client.GetAsync($"api/teams/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var jsonString = await response.Content.ReadAsStringAsync();
            var team = JsonSerializer.Deserialize<Team>(jsonString, _jsonOptions);
            if (team == null) return NotFound();
            await PopulateDropdownsAsync(team);
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTeam,Name,City,FoundedDate,IdCompetition,IdCountry,IdLeague")] Team team)
        {
            if (id != team.IdTeam) return NotFound();
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(team), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/teams/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el equipo desde la API.");
            }
            await PopulateDropdownsAsync(team);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/teams/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}