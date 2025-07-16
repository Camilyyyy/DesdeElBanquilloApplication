using DEAApi.Data;
using DEAModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesdeElBanquilloApplication.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public CountriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Crea un cliente HTTP preconfigurado para hablar con nuestra API
        private HttpClient GetApiClient()
        {
            // Usa el mismo cliente con nombre que configuraste en Program.cs
            return _httpClientFactory.CreateClient("api");
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var client = GetApiClient();
            // Llama al endpoint de la API para obtener todos los paisess
            var response = await client.GetAsync("api/countries");

            if (!response.IsSuccessStatusCode)
            {
                // Si la API falla, muestra una vista con una lista vacía.
                // Podrías también redirigir a una página de error personalizada.
                return View(new List<Country>());
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var competitions = JsonSerializer.Deserialize<List<Country>>(jsonString, _jsonOptions);

            return View(competitions);
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/countries/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var countries = JsonSerializer.Deserialize<Country>(jsonString, _jsonOptions);

            return View(countries);
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            // NOTA: Para que esta vista funcione correctamente, necesitarás cargar
            // las listas de Países, Temporadas y Federaciones desde la API
            // para poder mostrarlas en listas desplegables (dropdowns).
            return View();
        }

        // POST: Competitions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Country country)
        {
            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                var jsonContent = new StringContent(JsonSerializer.Serialize(country), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/countries", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al crear el pais desde la API.");
            }
            // NOTA: Si la validación falla, necesitarás volver a cargar los datos
            // para los dropdowns antes de devolver la vista.
            return View(country);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var client = GetApiClient();
            var response = await client.GetAsync($"api/countries/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var country = JsonSerializer.Deserialize<Country>(jsonString, _jsonOptions);

            // NOTA: Al igual que en Create, aquí deberías cargar los datos
            // para los dropdowns de Países, Temporadas y Federaciones.
            return View(country);
        }

        // POST: .../Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCountry,Name")] Country country) // <-- AÑADE IdCountry AQUÍ
        {
            if (id != country.IdCountry) return NotFound();

            if (ModelState.IsValid)
            {
                var client = GetApiClient();
                // Necesitarás enviar el objeto completo o al menos el id en el JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(country), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"api/countries/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Error al actualizar el pais desde la API.");
            }
            return View(country);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            // Reutilizamos la lógica del Details para obtener los datos de la competición
            // y mostrarlos en la página de confirmación de borrado.
            var client = GetApiClient();
            var response = await client.GetAsync($"api/countries/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var jsonString = await response.Content.ReadAsStringAsync();
            var country = JsonSerializer.Deserialize<Country>(jsonString, _jsonOptions);

            return View(country);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = GetApiClient();
            await client.DeleteAsync($"api/countries/{id}");

            // Redirige al Index independientemente de si la API tuvo éxito o no,
            // igual que en tu controlador de Positions.
            return RedirectToAction(nameof(Index));
        }
    }
}
