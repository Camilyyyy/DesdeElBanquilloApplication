// EN: Services/ApiService.cs (VERSIÓN FINAL CORREGIDA Y REFACTORIZADA)

using DEAMaui.Services;
using DEAModels;
using System.Diagnostics;
using System.Net.Http.Json;

namespace DEAMaui.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {

#if DEBUG

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(handler);
#else
            // En modo Release (producción), se usa el HttpClient normal y seguro.
            _httpClient = new HttpClient();
#endif
            // ===================================================================
            // FIN: ARREGLO FUNDAMENTAL
            // ===================================================================

            string baseAddress = GetBaseAddress();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        private string GetBaseAddress()
        {
            const string HttpPort = "5062";

            return DeviceInfo.Platform == DevicePlatform.Android
                ? $"http://10.0.2.2:{HttpPort}"
                : $"http://localhost:{HttpPort}";
        }



        #region Positions
        public Task<List<Position>> GetPositionsAsync() => GetAsync<Position>("api/positions");
        public Task<Position> GetPositionAsync(int id) => GetByIdAsync<Position>("api/positions", id);

        public Task<bool> AddPositionAsync(Position position) => PostAsync("api/positions", position);
        public Task<bool> UpdatePositionAsync(int id, Position position) => PutAsync($"api/positions/{id}", position);
        public Task<bool> DeletePositionAsync(int id) => DeleteAsync($"api/positions/{id}");
        #endregion

        #region Leagues
        public Task<List<League>> GetLeaguesAsync() => GetAsync<League>("api/leagues");
        public Task<League> GetLeagueAsync(int id) => GetByIdAsync<League>("api/leagues", id);
        public Task<bool> AddLeagueAsync(League league) => PostAsync("api/leagues", league);
        public Task<bool> UpdateLeagueAsync(int id, League league) => PutAsync($"api/leagues/{id}", league);
        public Task<bool> DeleteLeagueAsync(int id) => DeleteAsync($"api/leagues/{id}");
        #endregion

        #region Teams
        public Task<List<Team>> GetTeamsAsync() => GetAsync<Team>("api/teams");
        public Task<Team> GetTeamAsync(int id) => GetByIdAsync<Team>("api/teams", id);
        public Task<bool> AddTeamAsync(Team team) => PostAsync("api/teams", team);
        public Task<bool> UpdateTeamAsync(int id, Team team) => PutAsync($"api/teams/{id}", team);
        public Task<bool> DeleteTeamAsync(int id) => DeleteAsync($"api/teams/{id}");
        #endregion

        #region Players
        public Task<List<Player>> GetPlayersAsync() => GetAsync<Player>("api/players");
        public Task<Player> GetPlayerAsync(int id) => GetByIdAsync<Player>("api/players", id);
        public Task<bool> AddPlayerAsync(Player player) => PostAsync("api/players", player);
        public Task<bool> UpdatePlayerAsync(int id, Player player) => PutAsync($"api/players/{id}", player);
        public Task<bool> DeletePlayerAsync(int id) => DeleteAsync($"api/players/{id}");
        #endregion

        #region Competitions
        public Task<List<Competition>> GetCompetitionsAsync() => GetAsync<Competition>("api/competitions");
        public Task<Competition> GetCompetitionAsync(int id) => GetByIdAsync<Competition>("api/competitions", id);
        public Task<bool> AddCompetitionAsync(Competition competition) => PostAsync("api/competitions", competition);
        public Task<bool> UpdateCompetitionAsync(int id, Competition competition) => PutAsync($"api/competitions/{id}", competition);
        public Task<bool> DeleteCompetitionAsync(int id) => DeleteAsync($"api/competitions/{id}");
        #endregion

        #region Countries
        public Task<List<Country>> GetCountriesAsync() => GetAsync<Country>("api/countries");
        public Task<Country> GetCountryAsync(int id) => GetByIdAsync<Country>("api/countries", id);
        public Task<bool> AddCountryAsync(Country country) => PostAsync("api/countries", country);
        public Task<bool> UpdateCountryAsync(int id, Country country) => PutAsync($"api/countries/{id}", country);
        public Task<bool> DeleteCountryAsync(int id) => DeleteAsync($"api/countries/{id}");
        #endregion

        #region Federations
        public Task<List<Federation>> GetFederationsAsync() => GetAsync<Federation>("api/federations");
        public Task<Federation> GetFederationAsync(int id) => GetByIdAsync<Federation>("api/federations", id);
        public Task<bool> AddFederationAsync(Federation federation) => PostAsync("api/federations", federation);
        public Task<bool> UpdateFederationAsync(int id, Federation federation) => PutAsync($"api/federations/{id}", federation);
        public Task<bool> DeleteFederationAsync(int id) => DeleteAsync($"api/federations/{id}");
        #endregion

        #region Matches
        public Task<List<Match>> GetMatchesAsync() => GetAsync<Match>("api/matches");
        public Task<Match> GetMatchAsync(int id) => GetByIdAsync<Match>("api/matches", id);
        public Task<bool> AddMatchAsync(Match match) => PostAsync("api/matches", match);
        public Task<bool> UpdateMatchAsync(int id, Match match) => PutAsync($"api/matches/{id}", match);
        public Task<bool> DeleteMatchAsync(int id) => DeleteAsync($"api/matches/{id}");
        #endregion

        #region Seasons
        public Task<List<Season>> GetSeasonsAsync() => GetAsync<Season>("api/seasons");
        public Task<Season> GetSeasonAsync(int id) => GetByIdAsync<Season>("api/seasons", id);
        public Task<bool> AddSeasonAsync(Season season) => PostAsync("api/seasons", season);
        public Task<bool> UpdateSeasonAsync(int id, Season season) => PutAsync($"api/seasons/{id}", season);
        public Task<bool> DeleteSeasonAsync(int id) => DeleteAsync($"api/seasons/{id}");
        #endregion

        #region Stadiums
        public Task<List<Stadium>> GetStadiumsAsync() => GetAsync<Stadium>("api/stadiums");
        public Task<Stadium> GetStadiumAsync(int id) => GetByIdAsync<Stadium>("api/stadiums", id);
        public Task<bool> AddStadiumAsync(Stadium stadium) => PostAsync("api/stadiums", stadium);
        public Task<bool> UpdateStadiumAsync(int id, Stadium stadium) => PutAsync($"api/stadiums/{id}", stadium);
        public Task<bool> DeleteStadiumAsync(int id) => DeleteAsync($"api/stadiums/{id}");
        #endregion


        // =========================================================================
        // MÉTODOS AYUDANTES GENÉRICOS (PRIVADOS)
        // Aquí está la lógica centralizada para evitar la repetición de código.
        // =========================================================================

        private async Task<List<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<T>>();
            }
            catch (Exception ex) { Debug.WriteLine($"[API GET LIST ERROR] Endpoint: {endpoint} | Error: {ex.Message}"); }
            return new List<T>();
        }

        private async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{endpoint}/{id}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex) { Debug.WriteLine($"[API GET BY ID ERROR] Endpoint: {endpoint}/{id} | Error: {ex.Message}"); }
            return default(T); // Devuelve null para objetos
        }

        private async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"[API POST ERROR] Endpoint: {endpoint} | Error: {ex.Message}"); }
            return false;
        }

        private async Task<bool> PutAsync<T>(string endpointWithId, T data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpointWithId, data);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"[API PUT ERROR] Endpoint: {endpointWithId} | Error: {ex.Message}"); }
            return false;
        }

        private async Task<bool> DeleteAsync(string endpointWithId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpointWithId);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"[API DELETE ERROR] Endpoint: {endpointWithId} | Error: {ex.Message}"); }
            return false;
        }
    }
}