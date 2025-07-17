using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedLeague), "League")]
    public partial class LeagueDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        League league;

        [ObservableProperty]
        League receivedLeague;

        public ObservableCollection<Country> Countries { get; } = new();

        [ObservableProperty]
        Country selectedCountry;

        partial void OnSelectedCountryChanged(Country value)
        {
            if (value != null && League != null)
            {
                League.Country = value;
                League.IdCountry = value.IdCountry;
            }
        }

        public LeagueDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
            league = new League(); // Inicializar para evitar nulos
        }

        public async Task LoadCountriesAsync()
        {
            if (Countries.Any()) return;
            try
            {
                var countries = await _apiService.GetCountriesAsync();
                if (countries != null && countries.Any())
                {
                    foreach (var country in countries) Countries.Add(country);
                }
                UpdatePickerSelection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar países para el picker: {ex.Message}");
            }
        }

        partial void OnReceivedLeagueChanged(League value)
        {
            if (value != null)
            {
                League = value;
                Title = $"Editar {League.Name}";
            }
            else
            {
                League = new League { CreatedDate = DateTime.Now, IsActive = true };
                Title = "Nueva Liga";
            }
            UpdatePickerSelection();
        }

        private void UpdatePickerSelection()
        {
            if (Countries.Any() && League?.IdCountry > 0)
            {
                SetProperty(ref selectedCountry, Countries.FirstOrDefault(c => c.IdCountry == League.IdCountry));
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            if (SelectedCountry != null)
            {
                League.IdCountry = SelectedCountry.IdCountry;
            }

            if (string.IsNullOrWhiteSpace(League.Name) || League.IdCountry == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "El nombre y el país son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;
                bool success;

                // Creamos un objeto "limpio" para enviar a la API, con la propiedad de navegación en null.
                var leagueToSend = new League
                {
                    IdLeague = League.IdLeague,
                    Name = League.Name,
                    CreatedDate = League.CreatedDate,
                    IsActive = League.IsActive,
                    IdCountry = League.IdCountry,
                    Country = null // MUY IMPORTANTE
                };

                if (leagueToSend.IdLeague == 0)
                {
                    success = await _apiService.AddLeagueAsync(leagueToSend);
                }
                else
                {
                    success = await _apiService.UpdateLeagueAsync(leagueToSend.IdLeague, leagueToSend);
                }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar la liga.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar liga: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}