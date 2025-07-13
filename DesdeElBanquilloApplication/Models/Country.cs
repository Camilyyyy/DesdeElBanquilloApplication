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
        public  ICollection<Competition> Competitions { get; set; } = new List<Competition>();
        public  ICollection<Team> Teams { get; set; } = new List<Team>();
        public  ICollection<Player> Players { get; set; } = new List<Player>();
        public  ICollection<Federation> Federations { get; set; } = new List<Federation>();
        public  ICollection<League> Leagues { get; set; } = new List<League>();
    }
}
