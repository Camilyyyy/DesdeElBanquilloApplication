using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class PlayersViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Player> Players { get; } = new();

        public PlayersViewModel(IApiService apiService)
        {
            Title = "Jugadores";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetPlayersAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Players.Count != 0) Players.Clear();
                var players = await _apiService.GetPlayersAsync();
                foreach (var player in players)
                {
                    Players.Add(player);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar jugadores: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Player player)
        {
            await Shell.Current.GoToAsync(nameof(PlayerDetailPage), true, new Dictionary<string, object>
            {
                { "Player", player }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Player player)
        {
            if (player == null) return;
            await Shell.Current.GoToAsync(nameof(PlayerInfoPage), true, new Dictionary<string, object>
            {
                { "Player", player }
            });
        }

        [RelayCommand]
        async Task DeletePlayerAsync(Player player)
        {
            if (player == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar al jugador '{player.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeletePlayerAsync(player.IdPlayer);
                if (success)
                {
                    Players.Remove(player);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar al jugador.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar jugador: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}