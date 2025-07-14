using System.Collections.Generic;
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
        public string Name { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        [ForeignKey("Country")]
        public int IdCountry { get; set; }
        public virtual Country? Country { get; set; }

        // Propiedad de navegación NUEVA para la relación con Team
        [InverseProperty("League")]
        public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}