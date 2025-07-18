// EN: ViewModels/TeamInfoViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Team), "Team")]
    public partial class TeamInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Team team;

        partial void OnTeamChanged(Team value)
        {
            Title = value?.Name ?? "Información del Equipo";
        }
    }
}