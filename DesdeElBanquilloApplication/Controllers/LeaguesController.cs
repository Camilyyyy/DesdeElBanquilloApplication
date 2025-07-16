using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class LeaguesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public LeaguesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar Dropdown de Países ---
        // Este método obtiene la lista de países y la prepara para el dropdown.
        private async Task PopulateCountriesDropdownAsync(object? selectedCountry = null)
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/countries");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(json, _jsonOptions);
                ViewData["IdCountry"] = new SelectList(countries, "IdCountry", "Name", selectedCountry);
            }
        }

        // GET: Leagues
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/leagues");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<League>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var leagues = JsonSerializer.Deserialize<List<League>>(jsonString, _jsonOptions);

            return View(leagues);
        }

        // GET: Leagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/leagues/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var league = JsonSerializer.Deserialize<League>(jsonString, _jsonOptions);

            return View(league);
        }

        // GET: Leagues/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos el dropdown de países antes de mostrar el formulario.
            await PopulateCountriesDropdownAsync();
            return View();
        }

        // POST: Leagues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CreatedDate,IsActive,IdCountry")] League league)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(league), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/leagues", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear la liga desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown antes de mostrar la vista.
            await PopulateCountriesDropdownAsync(league.IdCountry);
            return View(league);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/leagues/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var league = JsonSerializer.Deserialize<League>(jsonString, _jsonOptions);

            if (league == null) return NotFound();

            // Cargamos el dropdown y pre-seleccionamos el país de la liga.
            await PopulateCountriesDropdownAsync(league.IdCountry);
            return View(league);
        }

        // POST: Leagues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLeague,Name,CreatedDate,IsActive,IdCountry")] League league)
        {
            if (id != league.IdLeague) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(league), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/leagues/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar la liga desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown.
            await PopulateCountriesDropdownAsync(league.IdCountry);
            return View(league);
        }

        // GET: Leagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica de Details para mostrar la página de confirmación.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/leagues/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var league = JsonSerializer.Deserialize<League>(jsonString, _jsonOptions);

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/leagues/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}