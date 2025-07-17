
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
            Title = "Pa�ses del Mundo";
            _apiService = apiService;
        }

        // =========================================================================
        // M�TODO PARA OBTENER LA LISTA DE PA�SES DESDE LA API
        // =========================================================================
        [RelayCommand]
        async Task GetCountriesAsync()
        {
            // Si ya se est� ejecutando una carga, no hacemos nada para evitar duplicados.
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

                // Llenamos nuestra colecci�n observable con los resultados.
                // La UI se actualizar� autom�ticamente.
                foreach (var country in countries)
                {
                    Countries.Add(country);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cr�tico al cargar pa�ses: {ex.Message}");
                await Shell.Current.DisplayAlert("Error de Conexi�n", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false; // Desactiva el ActivityIndicator, incluso si hubo un error.
            }
        }

        // =========================================================================
        // M�TODO PARA NAVEGAR A LA P�GINA DE DETALLES (A�ADIR O EDITAR)
        // =========================================================================
        [RelayCommand]
        async Task GoToDetailsAsync(Country country)
        {
            // Creamos un diccionario para pasar el objeto Country a la p�gina de detalles
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

        // Nuevo comando para ir a la p�gina de Ver Informaci�n
        [RelayCommand]
        async Task GoToInfoPageAsync(Country country)
        {
            if (country == null) return; // No tiene sentido ver info de un pa�s nulo

            await Shell.Current.GoToAsync(nameof(CountryInfoPage), true, new Dictionary<string, object>
            {
                { "Country", country }
            });
        }
        // ===============================================
        // M�TODO PARA ELIMINAR UN PA�S
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
                    "Confirmar Eliminaci�n",
                    $"�Est�s seguro de que quieres eliminar a {country.Name}?",
                    "S�, eliminar",
                    "No, cancelar");

                if (!userConfirmed)
                    return;

                IsBusy = true;

                bool success = await _apiService.DeleteCountryAsync(country.IdCountry);

                if (success)
                {
                    Countries.Remove(country);
                    await Shell.Current.DisplayAlert("�xito", "Pa�s eliminado correctamente.", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el pa�s desde la API.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar pa�s: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurri� un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}