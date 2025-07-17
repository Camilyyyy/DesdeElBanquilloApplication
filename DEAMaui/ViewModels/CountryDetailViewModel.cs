// EN: ViewModels/CountryDetailViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    // Este atributo indica que la propiedad 'Country' se llenará desde los parámetros de navegación
    [QueryProperty(nameof(Country), "Country")]
    public partial class CountryDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        // Esta propiedad contendrá el país que estamos creando o editando
        [ObservableProperty]
        Country country;

        public CountryDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Este método se dispara automáticamente cuando la propiedad 'Country' recibe un valor
        partial void OnCountryChanged(Country value)
        {

            Country ??= new Country();
            Title = Country.IdCountry == 0 ? "Nuevo País" : $"Editar {Country.Name}";
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Country.Name))
            {
                await Shell.Current.DisplayAlert("Error", "El nombre no puede estar vacío.", "OK");
                return;
            }

            if (IsBusy) return;

            try
            {
                IsBusy = true;
                bool success;

                if (Country.IdCountry == 0) // Es un país nuevo
                {
                    success = await _apiService.AddCountryAsync(Country);
                }
                else // Es un país existente
                {
                    success = await _apiService.UpdateCountryAsync(Country.IdCountry, Country);
                }

                if (success)
                {
                    await Shell.Current.DisplayAlert("Éxito", "País guardado correctamente.", "OK");
                    await Shell.Current.GoToAsync(".."); // Regresa a la página anterior
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo guardar el país.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar país: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}