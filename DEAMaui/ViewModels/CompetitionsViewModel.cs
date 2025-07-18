// EN: ViewModels/CompetitionsViewModel.cs

using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class CompetitionsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Competition> Competitions { get; } = new();

        public CompetitionsViewModel(IApiService apiService)
        {
            Title = "Competiciones";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetCompetitionsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Competitions.Count != 0) Competitions.Clear();
                var competitions = await _apiService.GetCompetitionsAsync();
                foreach (var competition in competitions)
                {
                    Competitions.Add(competition);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar competiciones: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Competition competition)
        {
            await Shell.Current.GoToAsync(nameof(CompetitionDetailPage), true, new Dictionary<string, object>
            {
                { "Competition", competition }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Competition competition)
        {
            if (competition == null) return;
            await Shell.Current.GoToAsync(nameof(CompetitionInfoPage), true, new Dictionary<string, object>
            {
                { "Competition", competition }
            });
        }

        [RelayCommand]
        async Task DeleteCompetitionAsync(Competition competition)
        {
            if (competition == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar la competición '{competition.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteCompetitionAsync(competition.IdCompetition);
                if (success)
                {
                    Competitions.Remove(competition);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la competición.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar competición: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}