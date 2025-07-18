using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedStadium), "Stadium")]
    public partial class StadiumDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Stadium stadium = new Stadium();

        [ObservableProperty]
        Stadium receivedStadium;

        public ObservableCollection<Team> Teams { get; } = new();

        [ObservableProperty]
        Team selectedTeam;

        public StadiumDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task LoadDependenciesAsync()
        {
            if (Teams.Any()) return;
            try
            {
                var teams = await _apiService.GetTeamsAsync();
                if (teams != null)
                {
                    foreach (var team in teams) Teams.Add(team);
                }

                if (Stadium?.IdTeam > 0)
                {
                    UpdatePickerSelection();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar equipos para el picker: {ex.Message}");
            }
        }

        partial void OnReceivedStadiumChanged(Stadium value)
        {
            if (value != null)
            {
                Stadium = value;
                Title = $"Editar {value.Name}";
            }
            else
            {
                Title = "Nuevo Estadio";
                Stadium.FoundedDate = DateTime.Now;
            }
        }

        private void UpdatePickerSelection()
        {
            if (Stadium != null && Teams.Any())
            {
                SelectedTeam = Teams.FirstOrDefault(t => t.IdTeam == Stadium.IdTeam);
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            Stadium.IdTeam = SelectedTeam?.IdTeam ?? 0;

            if (string.IsNullOrWhiteSpace(Stadium.Name) || Stadium.IdTeam == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "El nombre y el equipo son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var stadiumToSend = new Stadium
                {
                    IdStadium = Stadium.IdStadium,
                    Name = Stadium.Name,
                    FoundedDate = Stadium.FoundedDate,
                    Capacity = Stadium.Capacity,
                    IdTeam = Stadium.IdTeam,
                    Team = null
                };

                bool success;
                if (stadiumToSend.IdStadium == 0)
                {
                    success = await _apiService.AddStadiumAsync(stadiumToSend);
                }
                else
                {
                    success = await _apiService.UpdateStadiumAsync(stadiumToSend.IdStadium, stadiumToSend);
                }

                if (success) { await Shell.Current.GoToAsync(".."); }
                else { await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar el estadio.", "OK"); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar estadio: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally { IsBusy = false; }
        }
    }
}