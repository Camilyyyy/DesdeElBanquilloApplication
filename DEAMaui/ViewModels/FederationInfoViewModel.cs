using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Federation), "Federation")]
    public partial class FederationInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Federation federation;

        partial void OnFederationChanged(Federation value)
        {
            Title = value?.Name ?? "Información de la Federación";
        }
    }
}