using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using DEAMaui.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DEAMaui.ViewModels
{
    public partial class FederationsViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        public ObservableCollection<Federation> Federations { get; } = new();

        public FederationsViewModel(IApiService apiService)
        {
            Title = "Federaciones";
            _apiService = apiService;
        }

        [RelayCommand]
        async Task GetFederationsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                if (Federations.Count != 0) Federations.Clear();
                var federations = await _apiService.GetFederationsAsync();
                foreach (var federation in federations)
                {
                    Federations.Add(federation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar federaciones: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudieron obtener los datos de la API.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToEditPageAsync(Federation federation)
        {
            await Shell.Current.GoToAsync(nameof(FederationDetailPage), true, new Dictionary<string, object>
            {
                { "Federation", federation }
            });
        }

        [RelayCommand]
        async Task GoToInfoPageAsync(Federation federation)
        {
            if (federation == null) return;
            await Shell.Current.GoToAsync(nameof(FederationInfoPage), true, new Dictionary<string, object>
            {
                { "Federation", federation }
            });
        }

        [RelayCommand]
        async Task DeleteFederationAsync(Federation federation)
        {
            if (federation == null || IsBusy) return;
            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar", $"¿Eliminar la federación '{federation.Name}'?", "Sí", "No");
                if (!confirmed) return;

                IsBusy = true;
                bool success = await _apiService.DeleteFederationAsync(federation.IdFederation);
                if (success)
                {
                    Federations.Remove(federation);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar la federación.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al eliminar federación: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}