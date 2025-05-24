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
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        // Propiedades de navegación
        public virtual Team Team { get; set; }
        public virtual Position Position { get; set; }
        public virtual Country Country { get; set; }

        // Relaciones
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}
