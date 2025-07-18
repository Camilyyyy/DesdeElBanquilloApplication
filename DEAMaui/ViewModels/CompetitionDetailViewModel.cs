// EN: ViewModels/CompetitionDetailViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;


namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedCompetition), "Competition")]
    public partial class CompetitionDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Competition competition;

        [ObservableProperty]
        Competition receivedCompetition;

        // Propiedades para las listas y selecciones de los Pickers
        public ObservableCollection<Country> Countries { get; } = new();
        public ObservableCollection<Season> Seasons { get; } = new();
        public ObservableCollection<Federation> Federations { get; } = new();

        [ObservableProperty]
        Country selectedCountry;
        [ObservableProperty]
        Season selectedSeason;
        [ObservableProperty]
        Federation selectedFederation;

        public CompetitionDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
            competition = new Competition(); // Inicializar para evitar nulos
        }

        public async Task LoadDependenciesAsync()
        {
            if (Countries.Any()) return;
            try
            {
                var countriesTask = _apiService.GetCountriesAsync();
                var seasonsTask = _apiService.GetSeasonsAsync();
                var federationsTask = _apiService.GetFederationsAsync();
                await Task.WhenAll(countriesTask, seasonsTask, federationsTask);

                if (countriesTask.Result != null) foreach (var item in countriesTask.Result) Countries.Add(item);
                if (seasonsTask.Result != null) foreach (var item in seasonsTask.Result) Seasons.Add(item);
                if (federationsTask.Result != null) foreach (var item in federationsTask.Result) Federations.Add(item);

                UpdatePickerSelections();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar dependencias para la competición: {ex.Message}");
            }
        }

        partial void OnReceivedCompetitionChanged(Competition value)
        {
            if (value != null)
            {
                Competition = value;
                Title = $"Editar {Competition.Name}";
            }
            else
            {
                Competition = new Competition();
                Title = "Nueva Competición";
            }
            UpdatePickerSelections();
        }

        private void UpdatePickerSelections()
        {
            if (Competition == null) return;
            if (Countries.Any()) SelectedCountry = Countries.FirstOrDefault(c => c.IdCountry == Competition.IdCountry);
            if (Seasons.Any()) SelectedSeason = Seasons.FirstOrDefault(s => s.IdSeason == Competition.IdSeason);
            if (Federations.Any()) SelectedFederation = Federations.FirstOrDefault(f => f.IdFederation == Competition.IdFederation);
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // Sincronización Forzada de Claves Foráneas
            Competition.IdCountry = SelectedCountry?.IdCountry ?? 0;
            Competition.IdSeason = SelectedSeason?.IdSeason ?? 0;
            Competition.IdFederation = SelectedFederation?.IdFederation ?? 0;

            // Validación
            if (string.IsNullOrWhiteSpace(Competition.Name) || Competition.IdCountry == 0 || Competition.IdSeason == 0 || Competition.IdFederation == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "Nombre, País, Temporada y Federación son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var competitionToSend = new Competition
                {
                    IdCompetition = Competition.IdCompetition,
                    Name = Competition.Name,
                    IdCountry = Competition.IdCountry,
                    IdSeason = Competition.IdSeason,
                    IdFederation = Competition.IdFederation,
                    // Propiedades de navegación en null
                    Country = null,
                    Season = null,
                    Federation = null
                };

                bool success;
                if (competitionToSend.IdCompetition == 0)
                {
                    success = await _apiService.AddCompetitionAsync(competitionToSend);
                }
                else
                {
                    success = await _apiService.UpdateCompetitionAsync(competitionToSend.IdCompetition, competitionToSend);
                }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar la competición.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar competición: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}