using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("LeagueTables")]
    public class LeagueTable
    {
        [Key]
        public int IdLeagueTable { get; set; }

        [Range(1, 30)]
        [DisplayName("Posición")]
        public int Position { get; set; }

        [DisplayName("Partidos Jugados")]
        public int MatchesPlayed { get; set; } = 0;

        [DisplayName("Partidos Ganados")]
        public int MatchesWon { get; set; } = 0;

        [DisplayName("Partidos Empatados")]
        public int MatchesDrawn { get; set; } = 0;

        [DisplayName("Partidos Perdidos")]
        public int MatchesLost { get; set; } = 0;

        [DisplayName("Goles a Favor")]
        public int GoalsFor { get; set; } = 0;

        [DisplayName("Goles en Contra")]
        public int GoalsAgainst { get; set; } = 0;

        [DisplayName("Diferencia de Goles")]
        public int GoalDifference { get; set; } = 0;

        [DisplayName("Puntos")]
        public int Points { get; set; } = 0;

        [DisplayName("Última Actualización")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Claves foráneas
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [ForeignKey("Season")]
        public int SeasonId { get; set; }

        // Propiedades de navegación
        [DisplayName("Equipo")]
        public virtual Team Team { get; set; }

        [DisplayName("Temporada")]
        public virtual Season Season { get; set; }
    }
}
