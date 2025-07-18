using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class SeasonsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Season> Seasons { get; } = new();

        public SeasonsViewModel(IApiService apiService)
        {
            Title = "Temporadas";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetSeasonsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Seasons.Count != 0) Seasons.Clear();
                var seasons = await _apiService.GetSeasonsAsync();
                foreach (var season in seasons)
                {
                    Seasons.Add(season);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar temporadas: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Season season)
        {
            await Shell.Current.GoToAsync(nameof(SeasonDetailPage), true, new Dictionary<string, object>
            {
                { "Season", season }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Season season)
        {
            if (season == null) return;
            await Shell.Current.GoToAsync(nameof(SeasonInfoPage), true, new Dictionary<string, object>
            {
                { "Season", season }
            });
        }

        [RelayCommand]
        async Task DeleteSeasonAsync(Season season)
        {
            if (season == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar la temporada '{season.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteSeasonAsync(season.IdSeason);
                if (success)
                {
                    Seasons.Remove(season);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la temporada.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar temporada: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}