using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Administrators")]
    public class Administrator
    {
        [Key]
        public int IdAdministrator { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Admin")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Email Admin")]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Password Admin")]

        public string Password { get; set; }

        [DisplayName("Creacion Admin")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Estado Admin")]
        public bool IsActive { get; set; } = true;
    }
}
