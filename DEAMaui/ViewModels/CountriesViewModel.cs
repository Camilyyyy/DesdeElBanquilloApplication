
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class CountriesViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Country> Countries { get; } = new();

        public CountriesViewModel(IApiService apiService)
        {
            Title = "Países del Mundo";
            _apiService = apiService;
        }

        // =========================================================================
        // MÉTODO PARA OBTENER LA LISTA DE PAÍSES DESDE LA API
        // =========================================================================
        [RelayCommand]
        async Task GetCountriesAsync()
        {
            // Si ya se está ejecutando una carga, no hacemos nada para evitar duplicados.
            if (IsBusy)
                return;

            try
            {
                IsBusy = true; // Activa el ActivityIndicator en la UI

                // Limpiamos la lista actual antes de obtener los nuevos datos
                if (Countries.Count != 0)
                    Countries.Clear();

                // Llamamos al servicio de API para obtener los datos
                var countries = await _apiService.GetCountriesAsync();

                // Llenamos nuestra colección observable con los resultados.
                // La UI se actualizará automáticamente.
                foreach (var country in countries)
                {
                    Countries.Add(country);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error crítico al cargar países: {ex.Message}");
                await Shell.Current.DisplayAlert("Error de Conexión", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false; // Desactiva el ActivityIndicator, incluso si hubo un error.
            }
        }

        // =========================================================================
        // MÉTODO PARA NAVEGAR A LA PÁGINA DE DETALLES (AÑADIR O EDITAR)
        // =========================================================================
        [RelayCommand]
        async Task GoToDetailsAsync(Country country)
        {
            // Creamos un diccionario para pasar el objeto Country a la página de detalles
            var navigationParameter = new Dictionary<string, object>
            {
                // La clave "Country" debe coincidir con el [QueryProperty] en CountryDetailViewModel
                { "Country", country }
            };

            // Usamos nameof para evitar errores de tipeo en el nombre de la ruta
            await Shell.Current.GoToAsync(nameof(CountryDetailPage), true, navigationParameter);
        }
        [RelayCommand]
        async Task GoToEditPageAsync(Country country)
        {
            await Shell.Current.GoToAsync(nameof(CountryDetailPage), true, new Dictionary<string, object>
            {
                { "Country", country } // Pasa null para crear, o un objeto para editar
            });
        }

        // Nuevo comando para ir a la página de Ver Información
        [RelayCommand]
        async Task GoToInfoPageAsync(Country country)
        {
            if (country == null) return; // No tiene sentido ver info de un país nulo

            await Shell.Current.GoToAsync(nameof(CountryInfoPage), true, new Dictionary<string, object>
            {
                { "Country", country }
            });
        }
        // ===============================================
        // MÉTODO PARA ELIMINAR UN PAÍS
        // ===============================================
        [RelayCommand]
        async Task DeleteCountryAsync(Country country)
        {
            if (country == null)
                return;

            if (IsBusy)
                return;

            try
            {
                bool userConfirmed = await Shell.Current.DisplayAlert(
                    "Confirmar Eliminación",
                    $"¿Estás seguro de que quieres eliminar a {country.Name}?",
                    "Sí, eliminar",
                    "No, cancelar");

                if (!userConfirmed)
                    return;

                IsBusy = true;

                bool success = await _apiService.DeleteCountryAsync(country.IdCountry);

                if (success)
                {
                    Countries.Remove(country);
                    await Shell.Current.DisplayAlert("Éxito", "País eliminado correctamente.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el país desde la API.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar país: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}