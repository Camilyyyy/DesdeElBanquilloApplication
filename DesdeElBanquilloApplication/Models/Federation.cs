using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Federations")]
    public class Federation
    {
        [Key]
        public int IdFederation { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Federacion")]

        public string Name { get; set; }

        [StringLength(20)]
        public string Acronym { get; set; }

        [DisplayName("Fecha de Creacion Federacion")]
        public DateTime EstablishedDate { get; set; }

        // Clave foránea hacia 
        [Required] 
        [DisplayName("País")]
        public int IdCountry { get; set; }

        // Propiedad de navegación
        [ForeignKey("IdCountry")]
        public Country? Country { get; set; }

        // Relaciones
        public  ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
