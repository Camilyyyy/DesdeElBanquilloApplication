using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedTeam), "Team")]
    public partial class TeamDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Team team;

        [ObservableProperty]
        Team receivedTeam;

        // Propiedades para las listas y selecciones de los Pickers
        public ObservableCollection<Competition> Competitions { get; } = new();
        public ObservableCollection<Country> Countries { get; } = new();
        public ObservableCollection<League> Leagues { get; } = new();

        [ObservableProperty]
        Competition selectedCompetition;
        [ObservableProperty]
        Country selectedCountry;
        [ObservableProperty]
        League selectedLeague;

        public TeamDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
            team = new Team(); // Inicializar para evitar nulos
        }

        // Carga los datos necesarios para todos los Pickers
        public async Task LoadDependenciesAsync()
        {
            if (Countries.Any()) return;
            try
            {
                var competitionsTask = _apiService.GetCompetitionsAsync();
                var countriesTask = _apiService.GetCountriesAsync();
                var leaguesTask = _apiService.GetLeaguesAsync();
                await Task.WhenAll(competitionsTask, countriesTask, leaguesTask);

                if (competitionsTask.Result != null) foreach (var item in competitionsTask.Result) Competitions.Add(item);
                if (countriesTask.Result != null) foreach (var item in countriesTask.Result) Countries.Add(item);
                if (leaguesTask.Result != null) foreach (var item in leaguesTask.Result) Leagues.Add(item);

                UpdatePickerSelections();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar dependencias para el equipo: {ex.Message}");
            }
        }

        partial void OnReceivedTeamChanged(Team value)
        {
            if (value != null)
            {
                Team = value;
                Title = $"Editar {Team.Name}";
            }
            else
            {
                Team = new Team { FoundedDate = DateTime.Now.AddYears(-50) };
                Title = "Nuevo Equipo";
            }
            UpdatePickerSelections();
        }

        private void UpdatePickerSelections()
        {
            if (Team == null) return;
            if (Competitions.Any()) SelectedCompetition = Competitions.FirstOrDefault(c => c.IdCompetition == Team.IdCompetition);
            if (Countries.Any()) SelectedCountry = Countries.FirstOrDefault(c => c.IdCountry == Team.IdCountry);
            if (Leagues.Any()) SelectedLeague = Leagues.FirstOrDefault(l => l.IdLeague == Team.IdLeague);
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // Sincronización Forzada de Claves Foráneas
            Team.IdCompetition = SelectedCompetition?.IdCompetition ?? 0;
            Team.IdCountry = SelectedCountry?.IdCountry ?? 0;
            Team.IdLeague = SelectedLeague?.IdLeague ?? 0;

            // Validación
            if (string.IsNullOrWhiteSpace(Team.Name) || Team.IdCompetition == 0 || Team.IdCountry == 0 || Team.IdLeague == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "Nombre, Competición, País y Liga son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;

                // Creación del objeto "limpio" para enviar a la API
                var teamToSend = new Team
                {
                    IdTeam = Team.IdTeam,
                    Name = Team.Name,
                    City = Team.City,
                    FoundedDate = Team.FoundedDate,
                    IdCompetition = Team.IdCompetition,
                    IdCountry = Team.IdCountry,
                    IdLeague = Team.IdLeague,
                    // Propiedades de navegación en null
                    Competition = null,
                    Country = null,
                    League = null,
                    Stadium = null
                };

                bool success;
                if (teamToSend.IdTeam == 0)
                {
                    success = await _apiService.AddTeamAsync(teamToSend);
                }
                else
                {
                    success = await _apiService.UpdateTeamAsync(teamToSend.IdTeam, teamToSend);
                }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar el equipo.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar equipo: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}