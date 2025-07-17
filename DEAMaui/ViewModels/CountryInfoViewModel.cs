// EN: ViewModels/CountryInfoViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    // Recibe el objeto 'Country' a través de la navegación
    [QueryProperty(nameof(Country), "Country")]
    public partial class CountryInfoViewModel : BaseViewModel
    {
        // Esta es la propiedad que contendrá y expondrá el país
        [ObservableProperty]
        Country country;

        // Cuando la propiedad 'Country' recibe un valor, actualizamos el título de la página
        partial void OnCountryChanged(Country value)
        {
            Title = value?.Name ?? "Información del País";
        }
    }
}