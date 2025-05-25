using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        public int IdPlayer { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Jugador")]
        public string Name { get; set; }

        [Required]
        [Range(16, 50)]
        [DisplayName("Edad Jugador")]
        public int Age { get; set; }

        [Required]
        [Range(1, 99)]
        [DisplayName("Numero Camiseta Jugador")]
        public int JerseyNumber { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("Valor Mercado Jugador")]
        public decimal? MarketValue { get; set; }
        [Required]
        [DisplayName("Fecha Nacimiento Jugador")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DisplayName("Altura Jugador")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Height { get; set; } // en metros

        [Required]
        [DisplayName("Peso Jugador")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Weight { get; set; } // en kg

        // Claves foráneas
        [Required]
        [DisplayName("Equipo")]
        public int IdTeam { get; set; }

        [Required]
        [DisplayName("Posicion")]
        public int IdPosition { get; set; }

        [Required]
        [DisplayName("Pais")]
        public int IdCountry { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdTeam")]
        public Team? Team { get; set; }
        [ForeignKey("IdPosition")]
        public Position? Position { get; set; }
        [ForeignKey("IdCountry")]
        public Country? Country { get; set; }

        // Relaciones
        public ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}
