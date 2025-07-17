using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(League), "League")]
    public partial class LeagueInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        League league;

        partial void OnLeagueChanged(League value)
        {
            Title = value?.Name ?? "Información de la Liga";
        }
    }
}