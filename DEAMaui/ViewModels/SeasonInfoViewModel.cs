using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Season), "Season")]
    public partial class SeasonInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Season season;

        partial void OnSeasonChanged(Season value)
        {
            Title = value?.Name ?? "Información de la Temporada";
        }
    }
}