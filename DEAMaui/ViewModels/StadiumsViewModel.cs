using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class StadiumsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Stadium> Stadiums { get; } = new();

        public StadiumsViewModel(IApiService apiService)
        {
            Title = "Estadios";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetStadiumsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Stadiums.Count != 0) Stadiums.Clear();
                var stadiums = await _apiService.GetStadiumsAsync();
                foreach (var stadium in stadiums)
                {
                    Stadiums.Add(stadium);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar estadios: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Stadium stadium)
        {
            await Shell.Current.GoToAsync(nameof(StadiumDetailPage), true, new Dictionary<string, object>
            {
                { "Stadium", stadium }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Stadium stadium)
        {
            if (stadium == null) return;
            await Shell.Current.GoToAsync(nameof(StadiumInfoPage), true, new Dictionary<string, object>
            {
                { "Stadium", stadium }
            });
        }

        [RelayCommand]
        async Task DeleteStadiumAsync(Stadium stadium)
        {
            if (stadium == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar el estadio '{stadium.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteStadiumAsync(stadium.IdStadium);
                if (success)
                {
                    Stadiums.Remove(stadium);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el estadio.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar estadio: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}