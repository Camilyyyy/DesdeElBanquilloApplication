using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Pais")]

        public string Name { get; set; }

    

        // Relaciones
        public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
        public virtual ICollection<Federation> Federations { get; set; } = new List<Federation>();
    }
}
