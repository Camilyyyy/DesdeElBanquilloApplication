using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public SeasonsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar Dropdown de Ligas ---
        private async Task PopulateLeaguesDropdownAsync(object? selectedLeague = null)
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/leagues");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var leagues = JsonSerializer.Deserialize<List<League>>(json, _jsonOptions);
                ViewData["IdLeague"] = new SelectList(leagues, "IdLeague", "Name", selectedLeague);
            }
        }

        // GET: Seasons
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/seasons");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Season>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var seasons = JsonSerializer.Deserialize<List<Season>>(jsonString, _jsonOptions);

            return View(seasons);
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/seasons/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var season = JsonSerializer.Deserialize<Season>(jsonString, _jsonOptions);

            return View(season);
        }

        // GET: Seasons/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos el dropdown de ligas antes de mostrar el formulario.
            await PopulateLeaguesDropdownAsync();
            return View();
        }

        // POST: Seasons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,IsCurrent,TotalMatchdays,IdLeague")] Season season)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(season), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/seasons", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear la temporada desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown.
            await PopulateLeaguesDropdownAsync(season.IdLeague);
            return View(season);
        }

        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/seasons/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var season = JsonSerializer.Deserialize<Season>(jsonString, _jsonOptions);

            if (season == null) return NotFound();

            // Cargamos el dropdown y pre-seleccionamos la liga de la temporada.
            await PopulateLeaguesDropdownAsync(season.IdLeague);
            return View(season);
        }

        // POST: Seasons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeason,Name,StartDate,EndDate,IsCurrent,TotalMatchdays,IdLeague")] Season season)
        {
            if (id != season.IdSeason) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(season), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/seasons/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar la temporada desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown.
            await PopulateLeaguesDropdownAsync(season.IdLeague);
            return View(season);
        }

        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica de Details para la página de confirmación.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/seasons/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var season = JsonSerializer.Deserialize<Season>(jsonString, _jsonOptions);

            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/seasons/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}