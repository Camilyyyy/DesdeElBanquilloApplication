using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class PositionsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Position> Positions { get; } = new();

        public PositionsViewModel(IApiService apiService)
        {
            Title = "Posiciones de Juego";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetPositionsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Positions.Count != 0) Positions.Clear();
                var positions = await _apiService.GetPositionsAsync();
                foreach (var position in positions)
                {
                    Positions.Add(position);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar posiciones: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Position position)
        {
            await Shell.Current.GoToAsync(nameof(PositionDetailPage), true, new Dictionary<string, object>
            {
                { "Position", position }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Position position)
        {
            if (position == null) return;
            await Shell.Current.GoToAsync(nameof(PositionInfoPage), true, new Dictionary<string, object>
            {
                { "Position", position }
            });
        }

        [RelayCommand]
        async Task DeletePositionAsync(Position position)
        {
            if (position == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar la posición '{position.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeletePositionAsync(position.IdPosition);
                if (success)
                {
                    Positions.Remove(position);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la posición.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar posición: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}