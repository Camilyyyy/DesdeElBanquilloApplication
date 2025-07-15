using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesdeElBanquillo.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Programado"; // Finalizado, En juego, Programado
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }
        public int LeagueId { get; set; }
    }
}