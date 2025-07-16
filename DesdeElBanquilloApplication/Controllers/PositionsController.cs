using DEAModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace DesdeElBanquilloApplication.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public PositionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            return _httpClientFactory.CreateClient("api");
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            var response = await client.GetAsync("api/positions"); // Llama al endpoint de la API

            if (!response.IsSuccessStatusCode)
            {
                // Manejar el error, quizás mostrando una página de error
                return View(new List<Position>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var positions = JsonSerializer.Deserialize<List<Position>>(jsonString, _jsonOptions);

            return View(positions); // Envía la lista de posiciones (obtenida de la API) a la Vista
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/positions/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var position = JsonSerializer.Deserialize<Position>(jsonString, _jsonOptions);

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Position position) // Solo necesitamos el nombre
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/positions", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                // Si la API devuelve un error, podemos añadirlo al ModelState para mostrarlo al usuario
                ModelState.AddModelError(string.Empty, "Error al crear la posición desde la API.");
            }
            return View(position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/positions/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var position = JsonSerializer.Deserialize<Position>(jsonString, _jsonOptions);

            return View(position);
        }

        // POST: Positions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPosition,Name")] Position position)
        {
            if (id != position.IdPosition) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(position), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/positions/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar la posición desde la API.");
            }
            return View(position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            // Reutilizamos la lógica del método Edit para obtener la posición y mostrarla
            return await Edit(id);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            var response = await client.DeleteAsync($"api/positions/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            // Aquí podrías redirigir a una página de error o manejarlo de otra forma
            return RedirectToAction(nameof(Index));
        }
    }
}