// EN: ViewModels/TeamsViewModel.cs

using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class TeamsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Team> Teams { get; } = new();

        public TeamsViewModel(IApiService apiService)
        {
            Title = "Equipos";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetTeamsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Teams.Count != 0) Teams.Clear();
                var teams = await _apiService.GetTeamsAsync();
                foreach (var team in teams)
                {
                    Teams.Add(team);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar equipos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Team team)
        {
            await Shell.Current.GoToAsync(nameof(TeamDetailPage), true, new Dictionary<string, object>
            {
                { "Team", team }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Team team)
        {
            if (team == null) return;
            await Shell.Current.GoToAsync(nameof(TeamInfoPage), true, new Dictionary<string, object>
            {
                { "Team", team }
            });
        }

        [RelayCommand]
        async Task DeleteTeamAsync(Team team)
        {
            if (team == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar al equipo '{team.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteTeamAsync(team.IdTeam);
                if (success)
                {
                    Teams.Remove(team);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el equipo.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar equipo: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}