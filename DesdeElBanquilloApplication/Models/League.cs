using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Leagues")]

    public class League
    {
        [Key]
        public int IdLeague { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Liga")]
        public string Nombre { get; set; }

        [StringLength(50)]
        [DisplayName("Pais Liga")]
        public string Pais { get; set; }


        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Relación uno a muchos con Equipos
        public virtual ICollection<Team> Equipos { get; set; } = new List<Team>();

        // Relación uno a muchos con Temporadas
        public virtual ICollection<Temporada> Temporadas { get; set; } = new List<Temporada>();
    }
}
