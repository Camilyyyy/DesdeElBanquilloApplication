// EN: ViewModels/CountryDetailViewModel.cs (VERSIÓN CORREGIDA Y RECOMENDADA)

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{

    [QueryProperty(nameof(ReceivedCountry), "Country")]
    public partial class CountryDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Country country;


        [ObservableProperty]
        Country receivedCountry;

        public CountryDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;

            country = new Country();
        }

        partial void OnReceivedCountryChanged(Country value)
        {
            // Verificamos el objeto que LLEGÓ en la navegación ('value')
            if (value != null)
            {
                // Si llegó un país (estamos editando), lo copiamos a nuestra propiedad principal.
                Country = value;
                Title = $"Editar {Country.Name}";
            }
            else
            {
                // Si no llegó nada (estamos creando), nos aseguramos de que 'Country' sea un objeto nuevo.
                Country = new Country();
                Title = "Nuevo País";
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // El resto de la lógica de guardado no necesita cambios,
            // ya que se basa en la propiedad 'Country' que ahora siempre
            // estará correctamente inicializada.

            if (string.IsNullOrWhiteSpace(Country.Name))
            {
                await Shell.Current.DisplayAlert("Campo Requerido", "El nombre del país no puede estar vacío.", "OK");
                return;
            }

            if (IsBusy) return;

            try
            {
                IsBusy = true;
                bool success;

                if (Country.IdCountry == 0)
                {
                    success = await _apiService.AddCountryAsync(Country);
                }
                else
                {
                    success = await _apiService.UpdateCountryAsync(Country.IdCountry, Country);
                }

                if (success)
                {
                    await Shell.Current.DisplayAlert("Éxito", "País guardado correctamente.", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo guardar el país en la API.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar país: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado al comunicarse con la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}