using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesdeElBanquilloApplication.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Email User")]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        [DisplayName("Password User")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("Nombre User")]
        public string Nombre { get; set; }

        [DisplayName("Register Date User")]
        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [DisplayName("Estado User")]
        public bool IsAdmin { get; set; } = false;
    }
}
