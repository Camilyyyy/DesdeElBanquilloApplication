using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.RegularExpressions;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key]
        public int IdTeam { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Equipo")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Ciudad Equipo")]
        public string City { get; set; }

        [DisplayName("Fecha Fundacion Equipo")]
        public DateTime FoundedDate { get; set; }

        [StringLength(100)]
        public string Stadium { get; set; }

        // Claves foráneas
        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Propiedades de navegación
        public virtual Competition Competition { get; set; }
        public virtual Country Country { get; set; }

        // Relaciones
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

        // Relación uno a muchos como equipo local
        [InverseProperty("HomeTeam")]
        public virtual ICollection<Match> HomeMatches { get; set; } = new List<Match>();

        // Relación uno a muchos como equipo visitante
        [InverseProperty("AwayTeam")]
        public virtual ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}
