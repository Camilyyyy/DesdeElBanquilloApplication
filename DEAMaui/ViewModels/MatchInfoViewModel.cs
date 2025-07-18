// EN: ViewModels/MatchInfoViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Match), "Match")]
    public partial class MatchInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Match match;

        partial void OnMatchChanged(Match value)
        {
            Title = value != null ? $"{value.HomeTeam?.Name} vs {value.AwayTeam?.Name}" : "Información del Partido";
        }
    }
}