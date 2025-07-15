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



        // Claves foráneas
        [Required]
        [DisplayName("Competicion")]
        public int IdCompetition { get; set; }

        [Required]
        [DisplayName("País")]
        public int IdCountry { get; set; }

        [Required]
        [DisplayName("Liga")]
        public int IdLeague { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdCompetition")]
        public Competition? Competition { get; set; }
        [ForeignKey("IdCountry")]
        public Country? Country { get; set; }
        public Stadium? Stadium { get; set; }
        [ForeignKey("IdLeague")]
        public League? League { get; set; }


        // Relaciones
        public ICollection<Player> Players { get; set; } = new List<Player>();


        // Relación uno a muchos como equipo local
        [InverseProperty("HomeTeam")]
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();

        // Relación uno a muchos como equipo visitante
        [InverseProperty("AwayTeam")]
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}