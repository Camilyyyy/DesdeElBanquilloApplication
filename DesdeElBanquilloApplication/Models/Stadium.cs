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

        [Required]
        [DisplayName("Equipo")]
        public int IdTeam { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdTeam")]
        public Team? Team { get; set; }

        // Relaciones
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}