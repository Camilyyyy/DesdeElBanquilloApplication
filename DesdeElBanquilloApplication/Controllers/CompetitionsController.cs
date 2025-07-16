using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public CompetitionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar Datos de Dropdowns ---
        // Este método obtiene las listas de países, temporadas y federaciones
        // y las prepara para ser usadas en menús desplegables en las vistas.
        private async Task PopulateDropdownsAsync(Competition? competition = null)
        {
            var client = GetApiClient();

            // Cargar Países
            var countriesResponse = await client.GetAsync("api/countries");
            if (countriesResponse.IsSuccessStatusCode)
            {
                var countriesJson = await countriesResponse.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(countriesJson, _jsonOptions);
                // Pasa la lista a la vista a través de ViewData/ViewBag, seleccionando el valor actual si existe.
                ViewData["IdCountry"] = new SelectList(countries, "IdCountry", "Name", competition?.IdCountry);
            }

            // Cargar Temporadas (asumiendo endpoint 'api/seasons')
            var seasonsResponse = await client.GetAsync("api/seasons");
            if (seasonsResponse.IsSuccessStatusCode)
            {
                var seasonsJson = await seasonsResponse.Content.ReadAsStringAsync();
                var seasons = JsonSerializer.Deserialize<List<Season>>(seasonsJson, _jsonOptions);
                ViewData["IdSeason"] = new SelectList(seasons, "IdSeason", "Name", competition?.IdSeason);
            }

            // Cargar Federaciones (asumiendo endpoint 'api/federations')
            var federationsResponse = await client.GetAsync("api/federations");
            if (federationsResponse.IsSuccessStatusCode)
            {
                var federationsJson = await federationsResponse.Content.ReadAsStringAsync();
                var federations = JsonSerializer.Deserialize<List<Federation>>(federationsJson, _jsonOptions);
                ViewData["IdFederation"] = new SelectList(federations, "IdFederation", "Name", competition?.IdFederation);
            }
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/competitions");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Competition>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var competitions = JsonSerializer.Deserialize<List<Competition>>(jsonString, _jsonOptions);

            return View(competitions);
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/competitions/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var competition = JsonSerializer.Deserialize<Competition>(jsonString, _jsonOptions);

            return View(competition);
        }

        // GET: Competitions/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos los datos para los dropdowns antes de mostrar el formulario vacío.
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IdCountry,IdSeason,IdFederation")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(competition), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/competitions", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear la competición desde la API.");
            }

            // Si el modelo no es válido, volvemos a cargar los dropdowns antes de devolver la vista.
            await PopulateDropdownsAsync(competition);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/competitions/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var competition = JsonSerializer.Deserialize<Competition>(jsonString, _jsonOptions);

            if (competition == null) return NotFound();

            // Cargamos los datos para los dropdowns y pre-seleccionamos los valores de la competición a editar.
            await PopulateDropdownsAsync(competition);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompetition,Name,IdCountry,IdSeason,IdFederation")] Competition competition)
        {
            if (id != competition.IdCompetition) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(competition), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/competitions/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar la competición desde la API.");
            }

            // Si el modelo no es válido, volvemos a cargar los dropdowns antes de devolver la vista.
            await PopulateDropdownsAsync(competition);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // La página de confirmación de borrado solo necesita mostrar los detalles.
            // Es la misma lógica que el método Details().
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/competitions/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var competition = JsonSerializer.Deserialize<Competition>(jsonString, _jsonOptions);

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/competitions/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}