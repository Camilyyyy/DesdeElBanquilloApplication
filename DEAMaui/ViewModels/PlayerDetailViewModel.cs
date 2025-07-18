// EN: ViewModels/PlayerDetailViewModel.cs (VERSIÓN FINAL SÚPER-ROBUSTA)

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(PlayerToEdit), "Player")]
    public partial class PlayerDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        // Esta es la propiedad principal que usa la Vista.
        // Se inicializa aquí mismo para garantizar que NUNCA sea nula.
        [ObservableProperty]
        Player player = new Player();

        // Propiedad temporal SOLO para recibir datos de la navegación.
        [ObservableProperty]
        Player playerToEdit;

        public ObservableCollection<Team> Teams { get; } = new();
        public ObservableCollection<Position> Positions { get; } = new();
        public ObservableCollection<Country> Countries { get; } = new();

        [ObservableProperty]
        Team selectedTeam;
        [ObservableProperty]
        Position selectedPosition;
        [ObservableProperty]
        Country selectedCountry;

        public PlayerDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadDependenciesAsync()
        {
            if (Countries.Any()) return;
            try
            {
                var tasks = new
                {
                    Teams = _apiService.GetTeamsAsync(),
                    Positions = _apiService.GetPositionsAsync(),
                    Countries = _apiService.GetCountriesAsync()
                };
                await Task.WhenAll(tasks.Teams, tasks.Positions, tasks.Countries);

                if (tasks.Teams.Result != null) foreach (var item in tasks.Teams.Result) Teams.Add(item);
                if (tasks.Positions.Result != null) foreach (var item in tasks.Positions.Result) Positions.Add(item);
                if (tasks.Countries.Result != null) foreach (var item in tasks.Countries.Result) Countries.Add(item);

                // Después de cargar las listas, si estamos en modo edición,
                // pre-seleccionamos los pickers.
                if (Player?.IdPlayer > 0)
                {
                    UpdatePickerSelections();
                }
            }
            catch (Exception ex) { Debug.WriteLine($"Error al cargar dependencias: {ex.Message}"); }
        }

        // Se dispara SOLO cuando recibimos un jugador para editar.
        partial void OnPlayerToEditChanged(Player value)
        {
            if (value != null)
            {
                // MODO EDICIÓN
                Player = value; // Reemplazamos nuestro objeto 'Player' vacío con el que llega.
                Title = $"Editar {value.Name}";
            }
            else
            {
                // MODO CREAR
                // No hacemos nada aquí, porque 'Player' ya es un objeto nuevo y vacío.
                Title = "Nuevo Jugador";
                Player.BirthDate = DateTime.Now.AddYears(-20);
            }
        }

        private void UpdatePickerSelections()
        {
            if (Player == null) return;
            SelectedTeam = Teams.FirstOrDefault(t => t.IdTeam == Player.IdTeam);
            SelectedPosition = Positions.FirstOrDefault(p => p.IdPosition == Player.IdPosition);
            SelectedCountry = Countries.FirstOrDefault(c => c.IdCountry == Player.IdCountry);
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // El objeto 'Player' ahora está garantizado que no es nulo gracias a la inicialización en línea.
            // Sincronizamos los IDs desde los Pickers.
            Player.IdTeam = SelectedTeam?.IdTeam ?? 0;
            Player.IdPosition = SelectedPosition?.IdPosition ?? 0;
            Player.IdCountry = SelectedCountry?.IdCountry ?? 0;

            // La validación ahora es segura.
            if (string.IsNullOrWhiteSpace(Player.Name) || Player.IdTeam == 0 || Player.IdPosition == 0 || Player.IdCountry == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "Nombre, Equipo, Posición y País son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;

            try
            {
                IsBusy = true;

                var playerToSend = new Player
                {
                    IdPlayer = Player.IdPlayer,
                    Name = Player.Name,
                    Age = Player.Age,
                    JerseyNumber = Player.JerseyNumber,
                    MarketValue = Player.MarketValue,
                    BirthDate = Player.BirthDate,
                    Height = Player.Height,
                    Weight = Player.Weight,
                    IdTeam = Player.IdTeam,
                    IdPosition = Player.IdPosition,
                    IdCountry = Player.IdCountry,
                    Team = null,
                    Position = null,
                    Country = null
                };

                bool success;
                if (playerToSend.IdPlayer == 0) { success = await _apiService.AddPlayerAsync(playerToSend); }
                else { success = await _apiService.UpdatePlayerAsync(playerToSend.IdPlayer, playerToSend); }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar el jugador.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar jugador: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}