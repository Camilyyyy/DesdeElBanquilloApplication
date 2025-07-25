﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEAModels
{
    [Table("Positions")]
    public class Position
    {
        [Key]
        public int IdPosition { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nombre Posicion")]
        public string Name { get; set; } = string.Empty;

        // Relaciones
        public  ICollection<Player> Players { get; set; } = new List<Player>();
        public  ICollection<MatchPlayer> MatchPlayers { get; set; } = new List<MatchPlayer>();
    }
}
