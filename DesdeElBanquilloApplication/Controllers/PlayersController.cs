using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public PlayersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar todos los Dropdowns necesarios ---
        private async Task PopulateDropdownsAsync(Player? player = null)
        {
            var client = GetApiClient();

            // 1. Cargar Equipos
            var teamsResponse = await client.GetAsync("api/teams");
            if (teamsResponse.IsSuccessStatusCode)
            {
                var json = await teamsResponse.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<List<Team>>(json, _jsonOptions);
                ViewData["IdTeam"] = new SelectList(teams, "IdTeam", "Name", player?.IdTeam);
            }

            // 2. Cargar Posiciones
            var positionsResponse = await client.GetAsync("api/positions");
            if (positionsResponse.IsSuccessStatusCode)
            {
                var json = await positionsResponse.Content.ReadAsStringAsync();
                var positions = JsonSerializer.Deserialize<List<Position>>(json, _jsonOptions);
                ViewData["IdPosition"] = new SelectList(positions, "IdPosition", "Name", player?.IdPosition);
            }

            // 3. Cargar Países
            var countriesResponse = await client.GetAsync("api/countries");
            if (countriesResponse.IsSuccessStatusCode)
            {
                var json = await countriesResponse.Content.ReadAsStringAsync();
                var countries = JsonSerializer.Deserialize<List<Country>>(json, _jsonOptions);
                ViewData["IdCountry"] = new SelectList(countries, "IdCountry", "Name", player?.IdCountry);
            }
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/players");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Player>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var players = JsonSerializer.Deserialize<List<Player>>(jsonString, _jsonOptions);

            return View(players);
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/players/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var player = JsonSerializer.Deserialize<Player>(jsonString, _jsonOptions);

            return View(player);
        }

        // GET: Players/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos todos los dropdowns necesarios antes de mostrar el formulario.
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,JerseyNumber,MarketValue,BirthDate,Height,Weight,IdTeam,IdPosition,IdCountry")] Player player)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/players", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el jugador desde la API.");
            }

            // Si hay un error, volvemos a cargar los dropdowns.
            await PopulateDropdownsAsync(player);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/players/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var player = JsonSerializer.Deserialize<Player>(jsonString, _jsonOptions);

            if (player == null) return NotFound();

            // Cargamos los dropdowns y pre-seleccionamos los valores del jugador.
            await PopulateDropdownsAsync(player);
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPlayer,Name,Age,JerseyNumber,MarketValue,BirthDate,Height,Weight,IdTeam,IdPosition,IdCountry")] Player player)
        {
            if (id != player.IdPlayer) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/players/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el jugador desde la API.");
            }

            // Si hay un error, volvemos a cargar los dropdowns.
            await PopulateDropdownsAsync(player);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica de Details para la página de confirmación.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/players/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var player = JsonSerializer.Deserialize<Player>(jsonString, _jsonOptions);

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/players/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}