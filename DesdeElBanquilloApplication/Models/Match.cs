using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Matches")]
    public class Match
    {
        [Key]
        public int IdMatch { get; set; }

        [Required]
        [DisplayName("Fecha Partido")]
        public DateTime MatchDate { get; set; }

        [DisplayName("Goles en Casa")]
        public int? HomeGoals { get; set; }

        [DisplayName("Goles de Visitante")]
        public int? AwayGoals { get; set; }

        [Required]
        public MatchStatus Status { get; set; } = MatchStatus.Scheduled;



        [StringLength(50)]
        [Required]
        [DisplayName("Arbitro Partido")]
        public string Referee { get; set; }

        // Claves foráneas
        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }

        [ForeignKey("Stadium")]
        public int? StadiumId { get; set; }

        // Propiedades de navegación
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual Competition Competition { get; set; }
        public virtual Stadium Stadium { get; set; }

        // Relaciones
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }

    public enum MatchStatus
    {
        [Display(Name = "Programado")]
        Scheduled = 0,

        [Display(Name = "En Vivo")]
        Live = 1,

        [Display(Name = "Finalizado")]
        Finished = 2,

        [Display(Name = "Suspendido")]
        Suspended = 3,

        [Display(Name = "Cancelado")]
        Cancelled = 4,

        [Display(Name = "Reagendado")]
        Postponed = 5
    }
}
