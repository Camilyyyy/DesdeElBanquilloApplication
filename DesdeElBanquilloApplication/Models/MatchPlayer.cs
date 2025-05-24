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
        [ForeignKey("Match")]
        public int MatchId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }

        // Propiedades de navegación
        public virtual Match Match { get; set; }
        public virtual Player Player { get; set; }
        public virtual Position Position { get; set; }
    }
}
