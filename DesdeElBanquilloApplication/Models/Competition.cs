using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Competitions")]

    public class Competition
    {
        [Key]
        public int IdCompetition { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Competicion")]
        public string Name { get; set; }

        [StringLength(20)]
        [DisplayName("Temporada Competicion")]
        public string Season { get; set; }

        // Clave foránea hacia Country
        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Clave foránea hacia Federation
        [ForeignKey("Federation")]
        public int FederationId { get; set; }

        // Propiedades de navegación
        public virtual Country Country { get; set; }
        public virtual Federation Federation { get; set; }

        // Relaciones
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
