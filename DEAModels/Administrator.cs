using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEAModels
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        public int IdAdministrator { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Admin")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [DisplayName("Email Admin")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [DisplayName("Password Admin")]

        public string Password { get; set; } = string.Empty;

        [DisplayName("Creacion Admin")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Estado Admin")]
        public bool IsActive { get; set; } = true;
    }
}
