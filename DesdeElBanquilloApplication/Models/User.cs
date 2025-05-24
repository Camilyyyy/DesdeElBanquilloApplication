using System.ComponentModel.DataAnnotations;

namespace DesdeElBanquilloApplication.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public bool IsAdmin { get; set; } = false;
    }
}
