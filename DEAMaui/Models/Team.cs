using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesdeElBanquillo.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Stadium { get; set; } = string.Empty;
        public string Logo { get; set; } = "team_default.png";
        public int LeagueId { get; set; }
    }
}