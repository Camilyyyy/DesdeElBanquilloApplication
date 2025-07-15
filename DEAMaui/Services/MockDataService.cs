using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesdeElBanquillo.Models;

namespace DesdeElBanquillo.Services
{
    public class MockDataService
    {
        public List<League> GetLeagues()
        {
            return new List<League>
        {
            new League { Id = 1, Name = "Premier League", Country = "Inglaterra", Logo = "premier_league.png" },
            new League { Id = 2, Name = "Serie A", Country = "Italia", Logo = "serie_a.png" },
            new League { Id = 3, Name = "LigaPro Ecuador", Country = "Ecuador", Logo = "ligapro_ec.png" }
        };
        }

        public List<Team> GetTeams()
        {
            return new List<Team>
        {
            new Team { Id = 1, Name = "Manchester United", Stadium = "Old Trafford", LeagueId = 1 },
            new Team { Id = 2, Name = "Liverpool", Stadium = "Anfield", LeagueId = 1 },
            new Team { Id = 3, Name = "Juventus", Stadium = "Allianz Stadium", LeagueId = 2 },
            new Team { Id = 4, Name = "Barcelona SC", Stadium = "Monumental", LeagueId = 3 }
        };
        }

        public List<QuickAccessItem> GetQuickAccessItems()
        {
            return new List<QuickAccessItem>
        {
            new QuickAccessItem { Title = "Posiciones", Icon = "positions_icon.png", Description = "Tabla de posiciones actualizada" },
            new QuickAccessItem { Title = "Ligas", Icon = "leagues_icon.png", Description = "Ligas disponibles" },
            new QuickAccessItem { Title = "Equipos", Icon = "teams_icon.png", Description = "Información completa de equipos" },
            new QuickAccessItem { Title = "Jugadores", Icon = "players_icon.png", Description = "Detalles de jugadores" },
            new QuickAccessItem { Title = "Competiciones", Icon = "competitions_icon.png", Description = "Competiciones en curso" },
            new QuickAccessItem { Title = "Países", Icon = "countries_icon.png", Description = "Equipos por países" }
        };
        }
    }
}
