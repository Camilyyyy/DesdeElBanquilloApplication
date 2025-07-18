
using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Stadium), "Stadium")]
    public partial class StadiumInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Stadium stadium;

        partial void OnStadiumChanged(Stadium value)
        {
            Title = value?.Name ?? "Información del Estadio";
        }
    }
}