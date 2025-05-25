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
        [Required]
        [DisplayName("País")]
        public int IdCountry { get; set; }

        // Clave foránea hacia Federation
        [Required]
        [DisplayName("Federacion")]
        public int FederationId { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdCountry")]
        public Country? Country { get; set; }
        [ForeignKey("IdFederation")]
        public Federation? Federation { get; set; }

        // Relaciones
        public  ICollection<Team> Teams { get; set; } = new List<Team>();
        public  ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
