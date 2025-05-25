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
        public string Name { get; set; }

        [DisplayName("Fecha Crecion Liga")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [DisplayName("Liga Activa")]
        public bool IsActive { get; set; } = true;

        // Clave foránea hacia Country
        [Required]
        [DisplayName("País")]
        public int IdCountry { get; set; }
        // Propiedad de navegación

        [ForeignKey("IdCountry")]
        public Country? Country { get; set; }

        // Relación uno a muchos con Equipos
        public  ICollection<Team> Teams { get; set; } = new List<Team>();

        // Relación uno a muchos con Temporadas
        public  ICollection<Season> Seasons { get; set; } = new List<Season>();
    }
}
