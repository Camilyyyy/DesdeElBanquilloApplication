using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class MatchesViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Match> Matches { get; } = new();

        public MatchesViewModel(IApiService apiService)
        {
            Title = "Partidos";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetMatchesAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Matches.Count != 0) Matches.Clear();
                var matches = await _apiService.GetMatchesAsync();
                foreach (var match in matches)
                {
                    Matches.Add(match);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar partidos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Match match)
        {
            await Shell.Current.GoToAsync(nameof(MatchDetailPage), true, new Dictionary<string, object>
            {
                { "Match", match }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Match match)
        {
            if (match == null) return;
            await Shell.Current.GoToAsync(nameof(MatchInfoPage), true, new Dictionary<string, object>
            {
                { "Match", match }
            });
        }

        [RelayCommand]
        async Task DeleteMatchAsync(Match match)
        {
            if (match == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar el partido '{match.HomeTeam?.Name} vs {match.AwayTeam?.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteMatchAsync(match.IdMatch);
                if (success)
                {
                    Matches.Remove(match);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el partido.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar partido: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}