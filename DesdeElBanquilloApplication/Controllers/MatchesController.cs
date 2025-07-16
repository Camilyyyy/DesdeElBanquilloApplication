using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public MatchesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // --- Método Auxiliar para Cargar todos los Dropdowns necesarios ---
        private async Task PopulateDropdownsAsync(Match? match = null)
        {
            var client = GetApiClient();

            // 1. Cargar Equipos (para Equipo Local y Equipo Visitante)
            var teamsResponse = await client.GetAsync("api/teams");
            if (teamsResponse.IsSuccessStatusCode)
            {
                var json = await teamsResponse.Content.ReadAsStringAsync();
                var teams = JsonSerializer.Deserialize<List<Team>>(json, _jsonOptions);
                ViewData["IdHomeTeam"] = new SelectList(teams, "IdTeam", "Name", match?.IdHomeTeam);
                ViewData["IdAwayTeam"] = new SelectList(teams, "IdTeam", "Name", match?.IdAwayTeam);
            }

            // 2. Cargar Competiciones
            var competitionsResponse = await client.GetAsync("api/competitions");
            if (competitionsResponse.IsSuccessStatusCode)
            {
                var json = await competitionsResponse.Content.ReadAsStringAsync();
                var competitions = JsonSerializer.Deserialize<List<Competition>>(json, _jsonOptions);
                ViewData["IdCompetition"] = new SelectList(competitions, "IdCompetition", "Name", match?.IdCompetition);
            }

            // 3. Cargar Estadios
            var stadiumsResponse = await client.GetAsync("api/stadiums");
            if (stadiumsResponse.IsSuccessStatusCode)
            {
                var json = await stadiumsResponse.Content.ReadAsStringAsync();
                var stadiums = JsonSerializer.Deserialize<List<Stadium>>(json, _jsonOptions);
                ViewData["IdStadium"] = new SelectList(stadiums, "IdStadium", "Name", match?.IdStadium);
            }
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/matches");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Match>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var matches = JsonSerializer.Deserialize<List<Match>>(jsonString, _jsonOptions);

            return View(matches);
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/matches/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var match = JsonSerializer.Deserialize<Match>(jsonString, _jsonOptions);

            return View(match);
        }

        // GET: Matches/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos todos los dropdowns necesarios antes de mostrar el formulario.
            await PopulateDropdownsAsync();
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchDate,HomeGoals,AwayGoals,Status,Referee,IdHomeTeam,IdAwayTeam,IdCompetition,IdStadium")] Match match)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(match), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/matches", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el partido desde la API.");
            }

            // Si hay un error, volvemos a cargar los dropdowns.
            await PopulateDropdownsAsync(match);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/matches/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var match = JsonSerializer.Deserialize<Match>(jsonString, _jsonOptions);

            if (match == null) return NotFound();

            // Cargamos los dropdowns y pre-seleccionamos los valores del partido.
            await PopulateDropdownsAsync(match);
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMatch,MatchDate,HomeGoals,AwayGoals,Status,Referee,IdHomeTeam,IdAwayTeam,IdCompetition,IdStadium")] Match match)
        {
            if (id != match.IdMatch) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(match), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/matches/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el partido desde la API.");
            }

            // Si hay un error, volvemos a cargar los dropdowns.
            await PopulateDropdownsAsync(match);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica de Details para la página de confirmación.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/matches/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var match = JsonSerializer.Deserialize<Match>(jsonString, _jsonOptions);

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/matches/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}