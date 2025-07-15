using Newtonsoft.Json;

namespace DesdeElBanquilloApplication.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<T> GetFromApiAsync<T>(string url)
        {
            try
            {
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (HttpRequestException e)
            {
                // Maneja errores de conexión
                Console.WriteLine($"Request error: {e.Message}");
                return default(T);
            }
        }
    }
}
