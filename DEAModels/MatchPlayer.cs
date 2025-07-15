using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace DesdeElBanquilloApplication.Models
{
    [Table("MatchPlayers")]
    public class MatchPlayer
    {
        [Key]
        public int IdMatchPlayers { get; set; }

        [DisplayName("Goles")]
        public int Goals { get; set; } = 0;
        [DisplayName("Asistencias")]
        public int Assists { get; set; } = 0;
        [DisplayName("Tarjetas Amarillas")]
        public int YellowCards { get; set; } = 0;
        [DisplayName("Tarjetas Rojas")]
        public int RedCards { get; set; } = 0;
        [DisplayName("Minutos Jugados")]
        public int MinutesPlayed { get; set; } = 0;
        [DisplayName("Titular")]
        public bool IsStarter { get; set; } = false;
        [DisplayName("Minuto de Substitucion")]
        public int? SubstitutionMinute { get; set; }

        // Claves foráneas
        [Required]
        [DisplayName("Partido")]
        public int IdMatch { get; set; }

        [Required]
        [DisplayName("Jugador")]
        public int IdPlayer { get; set; }

        [Required]
        [DisplayName("Posicion")]
        public int IdPosition { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdMatch")]
        public Match? Match { get; set; }
        [ForeignKey("IdPlayer")]
        public Player? Player { get; set; }
        [ForeignKey("IdPosition")]
        public Position? Position { get; set; }
    }
}
