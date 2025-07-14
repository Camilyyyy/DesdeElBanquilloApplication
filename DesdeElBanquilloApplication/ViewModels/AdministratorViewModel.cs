using System.ComponentModel.DataAnnotations;

namespace DesdeElBanquilloApplication.ViewModels
{
    public class AdministratorViewModel
    {
        public int IdAdministrator { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }

}
