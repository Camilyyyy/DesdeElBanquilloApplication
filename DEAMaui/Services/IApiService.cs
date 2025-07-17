using DEAModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEAMaui.Services
{
    public interface IApiService
    {
        //Metodos para posiciones
        Task<List<Position>> GetPositionsAsync();
        Task<Position> GetPositionAsync(int id);
        Task<bool> AddPositionAsync(Position position);
        Task<bool> UpdatePositionAsync(int id, Position position); 
        Task<bool> DeletePositionAsync(int id);

        //Metodos para Ligas

        Task<List<League>> GetLeaguesAsync();
        Task<League> GetLeagueAsync(int id);
        Task<bool> AddLeagueAsync(League league);
        Task<bool> UpdateLeagueAsync(int id, League league);
        Task<bool> DeleteLeagueAsync(int id);

        //Metodos para Equipos

        Task<List<Team>> GetTeamsAsync();
        Task<Team> GetTeamAsync(int id);
        Task<bool> AddTeamAsync(Team team);
        Task<bool> UpdateTeamAsync(int id, Team team); 
        Task<bool> DeleteTeamAsync(int id);

        // Métodos para Jugadores
        Task<List<Player>> GetPlayersAsync();
        Task<Player> GetPlayerAsync(int id);
        Task<bool> AddPlayerAsync(Player player);
        Task<bool> UpdatePlayerAsync(int id, Player player); 
        Task<bool> DeletePlayerAsync(int id);

       
        //Metodos para Competiciones

        Task<List<Competition>> GetCompetitionsAsync();
        Task<Competition> GetCompetitionAsync(int id);
        Task<bool> AddCompetitionAsync(Competition competition);
        Task<bool> UpdateCompetitionAsync(int id, Competition competition); 
        Task<bool> DeleteCompetitionAsync(int id);

        //Metodos para Paises

        Task<List<Country>> GetCountriesAsync();
        Task<Country> GetCountryAsync(int id);
        Task<bool> AddCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(int id, Country country); 
        Task<bool> DeleteCountryAsync(int id);

        //Metodos para Federaciones

        Task<List<Federation>> GetFederationsAsync();
        Task<Federation> GetFederationAsync(int id);
        Task<bool> AddFederationAsync(Federation federation);
        Task<bool> UpdateFederationAsync(int id, Federation federation); 
        Task<bool> DeleteFederationAsync(int id);

        //Metodos para Partidos

        Task<List<Match>> GetMatchesAsync();
        Task<Match> GetMatchAsync(int id);
        Task<bool> AddMatchAsync(Match match);
        Task<bool> UpdateMatchAsync(int id, Match match); 
        Task<bool> DeleteMatchAsync(int id);

        //Metodos para Temporada

        Task<List<Season>> GetSeasonsAsync();
        Task<Season> GetSeasonAsync(int id);
        Task<bool> AddSeasonAsync(Season season);
        Task<bool> UpdateSeasonAsync(int id, Season season); 
        Task<bool> DeleteSeasonAsync(int id);

        //Metodos para Estadio

        Task<List<Stadium>> GetStadiumsAsync();
        Task<Stadium> GetStadiumAsync(int id);
        Task<bool> AddStadiumAsync(Stadium stadium);
        Task<bool> UpdateStadiumAsync(int id, Stadium stadium); 
        Task<bool> DeleteStadiumAsync(int id);
    }
}
