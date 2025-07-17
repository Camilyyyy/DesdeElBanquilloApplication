using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Position), "Position")]
    public partial class PositionInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Position position;

        partial void OnPositionChanged(Position value)
        {
            Title = value?.Name ?? "Información de la Posición";
        }
    }
}