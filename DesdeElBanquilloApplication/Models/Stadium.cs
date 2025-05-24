using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Stadiums")]
    public class Stadium
    {
        [Key]
        public int IdStadium { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Estadio")]
        public string Name { get; set; }

        [DisplayName("Fecha Creación Estadio")]
        public DateTime FoundedDate { get; set; }

        [Range(1, 200000)]
        [DisplayName("Capacidad")]
        public int Capacity { get; set; }

        // Claves foráneas

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        // Propiedades de navegación
        public virtual Team Team { get; set; }

        // Relaciones
        public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}