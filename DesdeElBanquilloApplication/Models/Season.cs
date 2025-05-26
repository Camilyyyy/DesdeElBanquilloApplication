using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Seasons")]
    public class Season
    {
        [Key]
        public int IdSeason { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Nombre Temporada")]
        public string Name { get; set; } // Ej: "2024-2025"

        [DisplayName("Fecha Inicio")]
        public DateTime StartDate { get; set; }

        [DisplayName("Fecha Fin")]
        public DateTime EndDate { get; set; }

        [DisplayName("Temporada Activa")]
        public bool IsCurrent { get; set; } = false;


        [Range(1, 50)]
        [DisplayName("Número de Jornadas")]
        public int TotalMatchdays { get; set; }

        // Clave foránea hacia League
        [Required]
        [DisplayName("Liga")]
        public int IdLeague { get; set; }

        // Propiedad de navegación hacia League
        [ForeignKey("IdLeague")]
        public League? League { get; set; }

        // Relación uno a muchos con Matches
        public ICollection<Match> Matches { get; set; } = new List<Match>();

        // Relación uno a muchos con tabla de posiciones
        public ICollection<LeagueTable> LeagueTables { get; set; } = new List<LeagueTable>();

        public  ICollection<Competition> Competitions { get; set; } = new List<Competition>();

    }
}
