
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel 
    {
        public HomePageViewModel()
        {
            Title = "Menú Principal";
        }

        [RelayCommand]
        async Task NavigateToAsync(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                Debug.WriteLine("Error: La ruta de navegación está vacía.");
                return;
            }

            try
            {
             
                await Shell.Current.GoToAsync($"//{route}");
            }
            catch (Exception ex)
            {
                // Esto es crucial. Si la ruta no está registrada, aquí veremos el error.
                Debug.WriteLine($"Error al navegar a la ruta '{route}': {ex.Message}");
                await Shell.Current.DisplayAlert("Error de Navegación", $"No se pudo encontrar la ruta: {route}. Asegúrate de que esté registrada en AppShell.", "OK");
            }
        }
    }
}