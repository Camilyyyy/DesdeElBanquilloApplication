using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesdeElBanquilloApplication.ViewModels
{
    public class LeagueViewModel
    {
        public int IdLeague { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombre Liga")]
        public string Name { get; set; } = null!;

        [DisplayName("Fecha Creación")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Liga Activa")]
        public bool IsActive { get; set; }

        [Required]
        [DisplayName("País")]
        public int IdCountry { get; set; }

        // Para mostrar el nombre del país en listados y detalles
        public string? CountryName { get; set; }
    }
}