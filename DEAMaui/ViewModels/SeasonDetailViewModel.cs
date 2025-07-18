// EN: ViewModels/SeasonDetailViewModel.cs (VERSIÓN FINAL Y CORREGIDA)

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    // Renombramos la QueryProperty para evitar conflictos
    [QueryProperty(nameof(ReceivedSeason), "Season")]
    public partial class SeasonDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        // La propiedad principal que usa la Vista. Se inicializa aquí para que NUNCA sea nula.
        [ObservableProperty]
        Season season = new Season();

        // Propiedad temporal SOLO para recibir datos de la navegación
        [ObservableProperty]
        Season receivedSeason;

        public ObservableCollection<League> Leagues { get; } = new();

        [ObservableProperty]
        League selectedLeague;

        public SeasonDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadDependenciesAsync()
        {
            if (Leagues.Any()) return;
            try
            {
                var leagues = await _apiService.GetLeaguesAsync();
                if (leagues != null)
                {
                    foreach (var league in leagues) Leagues.Add(league);
                }

                // Si estamos editando, pre-seleccionamos la liga
                if (Season?.IdLeague > 0)
                {
                    UpdatePickerSelection();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar ligas para el picker: {ex.Message}");
            }
        }

        // Se dispara SOLO cuando recibimos una temporada para editar
        partial void OnReceivedSeasonChanged(Season value)
        {
            if (value != null)
            {
                // MODO EDICIÓN: Reemplazamos nuestro objeto vacío con el que llega
                Season = value;
                Title = $"Editar {value.Name}";
            }
            else
            {
                // MODO CREAR: El objeto 'Season' ya existe, solo establecemos valores por defecto
                Title = "Nueva Temporada";
                Season.StartDate = DateTime.Now;
                Season.EndDate = DateTime.Now.AddMonths(10);
            }
        }

        private void UpdatePickerSelection()
        {
            if (Season != null && Leagues.Any())
            {
                SelectedLeague = Leagues.FirstOrDefault(l => l.IdLeague == Season.IdLeague);
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // Con la inicialización en línea, 'Season' ahora está garantizado que no es nulo.
            Season.IdLeague = SelectedLeague?.IdLeague ?? 0;

            if (string.IsNullOrWhiteSpace(Season.Name) || Season.IdLeague == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "El nombre y la liga son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var seasonToSend = new Season
                {
                    IdSeason = Season.IdSeason,
                    Name = Season.Name,
                    StartDate = Season.StartDate,
                    EndDate = Season.EndDate,
                    IsCurrent = Season.IsCurrent,
                    TotalMatchdays = Season.TotalMatchdays,
                    IdLeague = Season.IdLeague,
                    League = null
                };

                bool success;
                if (seasonToSend.IdSeason == 0)
                {
                    success = await _apiService.AddSeasonAsync(seasonToSend);
                }
                else
                {
                    success = await _apiService.UpdateSeasonAsync(seasonToSend.IdSeason, seasonToSend);
                }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar la temporada.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar temporada: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}