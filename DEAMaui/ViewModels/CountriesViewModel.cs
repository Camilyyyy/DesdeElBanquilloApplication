

using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DEAMaui.Views.Country;
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

        [RelayCommand]
        async Task GetCountriesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var countries = await _apiService.GetCountriesAsync();

                if (Countries.Count != 0)
                    Countries.Clear();

                foreach (var country in countries)
                    Countries.Add(country);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"No se pudieron cargar los países: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudo conectar con la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Country country)
        {
            // Navega a la página de detalles.
            // Si 'country' es null, es para crear uno nuevo.
            // Si tiene datos, es para editar.
            await Shell.Current.GoToAsync(nameof(CountryDetailPage), true, new Dictionary<string, object>
            {
                { "Country", country }
            });
        }
    }
}