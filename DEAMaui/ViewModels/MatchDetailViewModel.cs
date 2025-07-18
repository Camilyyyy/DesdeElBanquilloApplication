using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAMaui.Views;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedMatch), "Match")]
    public partial class MatchDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        private readonly ILocalFileService _localFileService;

        [ObservableProperty]
        Match match = new Match();

        [ObservableProperty]
        Match receivedMatch;

        // Propiedades para las listas y selecciones de los Pickers
        public ObservableCollection<Team> Teams { get; } = new();
        public ObservableCollection<Competition> Competitions { get; } = new();
        public ObservableCollection<Stadium> Stadiums { get; } = new();
        public List<MatchStatus> StatusOptions { get; } = Enum.GetValues(typeof(MatchStatus)).Cast<MatchStatus>().ToList();

        [ObservableProperty]
        Team selectedHomeTeam;
        [ObservableProperty]
        Team selectedAwayTeam;
        [ObservableProperty]
        Competition selectedCompetition;
        [ObservableProperty]
        Stadium selectedStadium;

        public MatchDetailViewModel(IApiService apiService, ILocalFileService localFileService)
        {
            _apiService = apiService;
            _localFileService = localFileService;
        }

        public async Task LoadDependenciesAsync()
        {
            if (Teams.Any()) return;
            try
            {
                var teamsTask = _apiService.GetTeamsAsync();
                var competitionsTask = _apiService.GetCompetitionsAsync();
                var stadiumsTask = _apiService.GetStadiumsAsync();
                await Task.WhenAll(teamsTask, competitionsTask, stadiumsTask);

                if (teamsTask.Result != null) foreach (var item in teamsTask.Result) Teams.Add(item);
                if (competitionsTask.Result != null) foreach (var item in competitionsTask.Result) Competitions.Add(item);
                if (stadiumsTask.Result != null) foreach (var item in stadiumsTask.Result) Stadiums.Add(item);

                UpdatePickerSelections();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar dependencias para el partido: {ex.Message}");
            }
        }

        partial void OnReceivedMatchChanged(Match value)
        {
            if (value != null)
            {
                Match = value;
                Title = $"Editar Partido";
            }
            else
            {
                Match = new Match { MatchDate = DateTime.Now };
                Title = "Nuevo Partido";
            }
            UpdatePickerSelections();
        }

        private void UpdatePickerSelections()
        {
            if (Match == null) return;
            if (Teams.Any())
            {
                SelectedHomeTeam = Teams.FirstOrDefault(t => t.IdTeam == Match.IdHomeTeam);
                SelectedAwayTeam = Teams.FirstOrDefault(t => t.IdTeam == Match.IdAwayTeam);
            }
            if (Competitions.Any()) SelectedCompetition = Competitions.FirstOrDefault(c => c.IdCompetition == Match.IdCompetition);
            if (Stadiums.Any()) SelectedStadium = Stadiums.FirstOrDefault(s => s.IdStadium == Match.IdStadium);
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            Match.IdHomeTeam = SelectedHomeTeam?.IdTeam ?? 0;
            Match.IdAwayTeam = SelectedAwayTeam?.IdTeam ?? 0;
            Match.IdCompetition = SelectedCompetition?.IdCompetition ?? 0;
            Match.IdStadium = SelectedStadium?.IdStadium ?? 0;

            if (Match.IdHomeTeam == 0 || Match.IdAwayTeam == 0 || Match.IdCompetition == 0 || Match.IdStadium == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "Todos los equipos, competición y estadio son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var matchToSend = new Match
                {
                    IdMatch = Match.IdMatch,
                    MatchDate = Match.MatchDate,
                    HomeGoals = Match.HomeGoals,
                    AwayGoals = Match.AwayGoals,
                    Status = Match.Status,
                    Referee = Match.Referee,
                    IdHomeTeam = Match.IdHomeTeam,
                    IdAwayTeam = Match.IdAwayTeam,
                    IdCompetition = Match.IdCompetition,
                    IdStadium = Match.IdStadium,
                    HomeTeam = null,
                    AwayTeam = null,
                    Competition = null,
                    Stadium = null
                };

                bool success;
                string action; // Variable para saber qué acción registrar

                if (matchToSend.IdMatch == 0)
                {
                    success = await _apiService.AddMatchAsync(matchToSend);
                    action = "CREADO";
                }
                else
                {
                    success = await _apiService.UpdateMatchAsync(matchToSend.IdMatch, matchToSend);
                    action = "ACTUALIZADO";
                }

                if (success)
                {
                    // --- INICIO DE LA MODIFICACIÓN ---
                    // 4. Si el guardado fue exitoso, lo registramos en el archivo local.
                    //    Necesitamos re-asignar las propiedades de navegación para que el log sea completo.
                    matchToSend.HomeTeam = SelectedHomeTeam;
                    matchToSend.AwayTeam = SelectedAwayTeam;
                    matchToSend.Competition = SelectedCompetition;
                    await _localFileService.LogMatchActionAsync(action, matchToSend);
                    // --- FIN DE LA MODIFICACIÓN ---

                    await Shell.Current.GoToAsync($"//{nameof(MatchesPage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar el partido.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar partido: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}