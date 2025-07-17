// EN: ViewModels/FederationDetailViewModel.cs (VERSIÓN FINAL Y COMPLETA)

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DEAMaui.Services;
using DEAModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(ReceivedFederation), "Federation")]
    public partial class FederationDetailViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        [ObservableProperty]
        Federation federation;

        [ObservableProperty]
        Federation receivedFederation;

        public ObservableCollection<Country> Countries { get; } = new();

        [ObservableProperty]
        Country selectedCountry;

        // Este método se dispara cuando el usuario selecciona un país en el Picker
        partial void OnSelectedCountryChanged(Country value)
        {
            // Mantenemos sincronizado el objeto principal para la UI
            if (value != null && Federation != null)
            {
                Federation.Country = value;
                Federation.IdCountry = value.IdCountry;
            }
        }

        public FederationDetailViewModel(IApiService apiService)
        {
            _apiService = apiService;
            federation = new Federation();
        }

        public async Task LoadCountriesAsync()
        {
            if (Countries.Any()) return;

            try
            {
                var countries = await _apiService.GetCountriesAsync();
                if (countries != null && countries.Any())
                {
                    foreach (var country in countries) Countries.Add(country);
                }
                UpdatePickerSelection();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar países: {ex.Message}");
            }
        }

        partial void OnReceivedFederationChanged(Federation value)
        {
            if (value != null)
            {
                Federation = value;
                Title = $"Editar {Federation.Name}";
            }
            else
            {
                Federation = new Federation { EstablishedDate = DateTime.Now };
                Title = "Nueva Federación";
            }
            UpdatePickerSelection();
        }

        private void UpdatePickerSelection()
        {
            if (Countries.Any() && Federation?.IdCountry > 0)
            {
                // Usamos SetProperty para evitar bucles de notificación innecesarios
                SetProperty(ref selectedCountry, Countries.FirstOrDefault(c => c.IdCountry == Federation.IdCountry));
            }
        }

        [RelayCommand]
        async Task SaveAsync()
        {
            // 1. Forzamos la sincronización por si el evento no se disparó correctamente
            if (SelectedCountry != null)
            {
                Federation.IdCountry = SelectedCountry.IdCountry;
            }

            // 2. Validación
            if (string.IsNullOrWhiteSpace(Federation.Name) || Federation.IdCountry == 0)
            {
                await Shell.Current.DisplayAlert("Campos Requeridos", "El nombre y el país son obligatorios.", "OK");
                return;
            }

            if (IsBusy) return;

            try
            {
                IsBusy = true;
                bool success;

                // ==========================================================
                //           INICIO: SOLUCIÓN CLAVE
                // ==========================================================

                // Creamos un objeto de transferencia de datos (DTO) "limpio".
                // Esto evita problemas de seguimiento de entidades en la API.
                var federationToSend = new Federation
                {
                    IdFederation = Federation.IdFederation,
                    Name = Federation.Name,
                    Acronym = Federation.Acronym,
                    EstablishedDate = Federation.EstablishedDate,
                    IdCountry = Federation.IdCountry, // Enviamos la clave foránea
                    Country = null // ¡MUY IMPORTANTE! La propiedad de navegación va como null
                };

                // ==========================================================
                //           FIN: SOLUCIÓN CLAVE
                // ==========================================================

                // 3. Enviamos el objeto limpio a la API
                if (federationToSend.IdFederation == 0)
                {
                    success = await _apiService.AddFederationAsync(federationToSend);
                }
                else
                {
                    success = await _apiService.UpdateFederationAsync(federationToSend.IdFederation, federationToSend);
                }

                if (success)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // Si llegamos aquí, es porque la API devolvió un error (ej. 400, 500)
                    await Shell.Current.DisplayAlert("Error de API", "No se pudo guardar la federación.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al guardar federación: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un error inesperado.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}