using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedPosition), "Position")]
    public partial class PositionDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Position position;

        [ObservableProperty]
        Position receivedPosition;

        public PositionDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
            position = new Position();
        }

        partial void OnReceivedPositionChanged(Position value)
        {
            if (value != null)
            {
                Position = value;
                Title = $"Editar {Position.Name}";
            }
            else
            {
                Position = new Position();
                Title = "Nueva Posición";
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Position.Name))
            {
                await Shell.Current.DisplayAlert("Error", "El nombre no puede estar vacío.", "OK");
                return;
            }
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                bool success;
                if (Position.IdPosition == 0)
                {
                    success = await _apiService.AddPositionAsync(Position);
                }
                else
                {
                    success = await _apiService.UpdatePositionAsync(Position.IdPosition, Position);
                }

                if (success)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo guardar la posición.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar posición: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}