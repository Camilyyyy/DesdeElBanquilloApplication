using DEAMaui.Services; // Asegúrate que este using apunte a tu interfaz
using DEAModels;
using System.Diagnostics;
using System.Net.Http.Json; // ¡Este using es crucial para los métodos .GetFromJsonAsync, .PostAsJsonAsync, etc.!

namespace DEAMaui.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();

            // Determina la dirección base correcta para la plataforma actual
            string baseAddress = GetBaseAddress();
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        private string GetBaseAddress()
        {
            return DeviceInfo.Platform == DevicePlatform.Android
                ? "https://10.0.2.2:5062"
                : "https://localhost:5062";
        }

  
        // MÉTODOS PARA POSITIONS

        public async Task<List<Position>> GetPositionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/positions");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<List<Position>>();
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return new List<Position>();
        }

        public async Task<Position> GetPositionAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/positions/{id}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<Position>();
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return null;
        }

        public async Task<bool> AddPositionAsync(Position position)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/positions", position);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

        public async Task<bool> UpdatePositionAsync(int id, Position position)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/positions/{id}", position);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

        public async Task<bool> DeletePositionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/positions/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

        // MÉTODOS PARA LEAGUES


        public async Task<List<League>> GetLeaguesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/leagues");
                if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<League>>();
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return new List<League>();
        }

        public async Task<League> GetLeagueAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/leagues/{id}");
                if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<League>();
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return null;
        }

        public async Task<bool> AddLeagueAsync(League league)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/leagues", league);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

        public async Task<bool> UpdateLeagueAsync(int id, League league)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/leagues/{id}", league);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

        public async Task<bool> DeleteLeagueAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/leagues/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); }
            return false;
        }

      
        // MÉTODOS PARA TEAMS

        public async Task<List<Team>> GetTeamsAsync() { try { var response = await _httpClient.GetAsync("api/teams"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Team>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Team>(); }
        public async Task<Team> GetTeamAsync(int id) { try { var response = await _httpClient.GetAsync($"api/teams/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Team>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddTeamAsync(Team team) { try { var response = await _httpClient.PostAsJsonAsync("api/teams", team); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateTeamAsync(int id, Team team) { try { var response = await _httpClient.PutAsJsonAsync($"api/teams/{id}", team); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteTeamAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/teams/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

        
        // MÉTODOS PARA PLAYERS
 
        public async Task<List<Player>> GetPlayersAsync() { try { var response = await _httpClient.GetAsync("api/players"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Player>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Player>(); }
        public async Task<Player> GetPlayerAsync(int id) { try { var response = await _httpClient.GetAsync($"api/players/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Player>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddPlayerAsync(Player player) { try { var response = await _httpClient.PostAsJsonAsync("api/players", player); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdatePlayerAsync(int id, Player player) { try { var response = await _httpClient.PutAsJsonAsync($"api/players/{id}", player); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeletePlayerAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/players/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

       
        // MÉTODOS PARA COMPETITIONS
 
        public async Task<List<Competition>> GetCompetitionsAsync() { try { var response = await _httpClient.GetAsync("api/competitions"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Competition>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Competition>(); }
        public async Task<Competition> GetCompetitionAsync(int id) { try { var response = await _httpClient.GetAsync($"api/competitions/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Competition>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddCompetitionAsync(Competition competition) { try { var response = await _httpClient.PostAsJsonAsync("api/competitions", competition); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateCompetitionAsync(int id, Competition competition) { try { var response = await _httpClient.PutAsJsonAsync($"api/competitions/{id}", competition); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteCompetitionAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/competitions/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

        // MÉTODOS PARA COUNTRIES
  
        public async Task<List<Country>> GetCountriesAsync() { try { var response = await _httpClient.GetAsync("api/countries"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Country>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Country>(); }
        public async Task<Country> GetCountryAsync(int id) { try { var response = await _httpClient.GetAsync($"api/countries/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Country>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddCountryAsync(Country country) { try { var response = await _httpClient.PostAsJsonAsync("api/countries", country); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateCountryAsync(int id, Country country) { try { var response = await _httpClient.PutAsJsonAsync($"api/countries/{id}", country); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteCountryAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/countries/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

      

        // (Añadidos para completar la interfaz)
        public async Task<List<Federation>> GetFederationsAsync() { try { var response = await _httpClient.GetAsync("api/federations"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Federation>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Federation>(); }
        public async Task<Federation> GetFederationAsync(int id) { try { var response = await _httpClient.GetAsync($"api/federations/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Federation>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddFederationAsync(Federation federation) { try { var response = await _httpClient.PostAsJsonAsync("api/federations", federation); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateFederationAsync(int id, Federation federation) { try { var response = await _httpClient.PutAsJsonAsync($"api/federations/{id}", federation); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteFederationAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/federations/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

        public async Task<List<Match>> GetMatchesAsync() { try { var response = await _httpClient.GetAsync("api/matches"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Match>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Match>(); }
        public async Task<Match> GetMatchAsync(int id) { try { var response = await _httpClient.GetAsync($"api/matches/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Match>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddMatchAsync(Match match) { try { var response = await _httpClient.PostAsJsonAsync("api/matches", match); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateMatchAsync(int id, Match match) { try { var response = await _httpClient.PutAsJsonAsync($"api/matches/{id}", match); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteMatchAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/matches/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

        public async Task<List<Season>> GetSeasonsAsync() { try { var response = await _httpClient.GetAsync("api/seasons"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Season>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Season>(); }
        public async Task<Season> GetSeasonAsync(int id) { try { var response = await _httpClient.GetAsync($"api/seasons/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Season>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddSeasonAsync(Season season) { try { var response = await _httpClient.PostAsJsonAsync("api/seasons", season); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateSeasonAsync(int id, Season season) { try { var response = await _httpClient.PutAsJsonAsync($"api/seasons/{id}", season); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteSeasonAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/seasons/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }

        public async Task<List<Stadium>> GetStadiumsAsync() { try { var response = await _httpClient.GetAsync("api/stadiums"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<List<Stadium>>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return new List<Stadium>(); }
        public async Task<Stadium> GetStadiumAsync(int id) { try { var response = await _httpClient.GetAsync($"api/stadiums/{id}"); if (response.IsSuccessStatusCode) return await response.Content.ReadFromJsonAsync<Stadium>(); } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return null; }
        public async Task<bool> AddStadiumAsync(Stadium stadium) { try { var response = await _httpClient.PostAsJsonAsync("api/stadiums", stadium); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> UpdateStadiumAsync(int id, Stadium stadium) { try { var response = await _httpClient.PutAsJsonAsync($"api/stadiums/{id}", stadium); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
        public async Task<bool> DeleteStadiumAsync(int id) { try { var response = await _httpClient.DeleteAsync($"api/stadiums/{id}"); return response.IsSuccessStatusCode; } catch (Exception ex) { Debug.WriteLine($"ERROR: {ex.Message}"); } return false; }
    }
}