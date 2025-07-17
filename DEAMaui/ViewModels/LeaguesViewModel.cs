using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class LeaguesViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<League> Leagues { get; } = new();

        public LeaguesViewModel(IApiService apiService)
        {
            Title = "Ligas";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetLeaguesAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Leagues.Count != 0) Leagues.Clear();
                var leagues = await _apiService.GetLeaguesAsync();
                foreach (var league in leagues)
                {
                    Leagues.Add(league);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar ligas: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(League league)
        {
            await Shell.Current.GoToAsync(nameof(LeagueDetailPage), true, new Dictionary<string, object>
            {
                { "League", league }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(League league)
        {
            if (league == null) return;
            await Shell.Current.GoToAsync(nameof(LeagueInfoPage), true, new Dictionary<string, object>
            {
                { "League", league }
            });
        }

        [RelayCommand]
        async Task DeleteLeagueAsync(League league)
        {
            if (league == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar la liga '{league.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteLeagueAsync(league.IdLeague);
                if (success)
                {
                    Leagues.Remove(league);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la liga.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar liga: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}