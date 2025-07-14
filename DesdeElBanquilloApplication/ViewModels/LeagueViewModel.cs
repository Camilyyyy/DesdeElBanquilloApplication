using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesdeElBanquilloApplication.ViewModels
{
    public class LeagueViewModel
    {
        public int IdLeague { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [DisplayName("Nombre de Liga")]
        public string Name { get; set; } = null!;

        [DisplayName("Fecha de Creación")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Activa")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Seleccione un país")]
        [DisplayName("País")]
        public int IdCountry { get; set; }

        [DisplayName("País")]
        public string? CountryName { get; set; }
    }
}