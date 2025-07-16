using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList
using System.Text;
using System.Text.Json;

namespace DesdeElBanquilloApplication.Controllers
{
    public class FederationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public FederationsController(IHttpClientFactory httpClientFactory)
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

        // GET: Federations
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/federations");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<Federation>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var federations = JsonSerializer.Deserialize<List<Federation>>(jsonString, _jsonOptions);

            return View(federations);
        }

        // GET: Federations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/federations/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var federation = JsonSerializer.Deserialize<Federation>(jsonString, _jsonOptions);

            return View(federation);
        }

        // GET: Federations/Create
        public async Task<IActionResult> Create()
        {
            // Cargamos el dropdown de países antes de mostrar el formulario.
            await PopulateCountriesDropdownAsync();
            return View();
        }

        // POST: Federations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Acronym,EstablishedDate,IdCountry")] Federation federation)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(federation), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/federations", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear la federación desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown antes de mostrar la vista.
            await PopulateCountriesDropdownAsync(federation.IdCountry);
            return View(federation);
        }

        // GET: Federations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/federations/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var federation = JsonSerializer.Deserialize<Federation>(jsonString, _jsonOptions);

            if (federation == null) return NotFound();

            // Cargamos el dropdown y pre-seleccionamos el país de la federación.
            await PopulateCountriesDropdownAsync(federation.IdCountry);
            return View(federation);
        }

        // POST: Federations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFederation,Name,Acronym,EstablishedDate,IdCountry")] Federation federation)
        {
            if (id != federation.IdFederation) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(federation), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/federations/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar la federación desde la API.");
            }

            // Si hay un error, volvemos a cargar el dropdown.
            await PopulateCountriesDropdownAsync(federation.IdCountry);
            return View(federation);
        }

        // GET: Federations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica de Details para mostrar la página de confirmación.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/federations/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var federation = JsonSerializer.Deserialize<Federation>(jsonString, _jsonOptions);

            return View(federation);
        }

        // POST: Federations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/federations/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}