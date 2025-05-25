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
        [Required]
        [DisplayName("Equipo Local")]
        public int IdHomeTeam { get; set; }

        [Required]
        [DisplayName("Equipo Visitante")]
        public int IdAwayTeam { get; set; }

        [Required]
        [DisplayName("Competicion")]
        public int IdCompetition { get; set; }

        [Required]
        [DisplayName("Estadio")]
        public int IdStadium { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdHomeTeam")]
        public Team? HomeTeam { get; set; }
        [ForeignKey("IdAwayTeam")]
        public Team? AwayTeam { get; set; }
        [ForeignKey("IdCompetition")]
        public Competition? Competition { get; set; }
        [ForeignKey("IdStadium")]
        public Stadium? Stadium { get; set; }

        // Relaciones
        public  ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
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
