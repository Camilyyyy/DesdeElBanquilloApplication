using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DEAModels;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://tu-api-url.com/"; // <-- CAMBIA ESTO por la URL de tu API

    public ApiService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl) };
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Country>>("api/countries");
    }

    public async Task<Country> GetCountryAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Country>($"api/countries/{id}");
    }

    public async Task AddCountryAsync(Country country)
    {
        await _httpClient.PostAsJsonAsync("api/countries", country);
    }

    public async Task UpdateCountryAsync(int id, Country country)
    {
        await _httpClient.PutAsJsonAsync($"api/countries/{id}", country);
    }

    public async Task DeleteCountryAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/countries/{id}");
    }
}