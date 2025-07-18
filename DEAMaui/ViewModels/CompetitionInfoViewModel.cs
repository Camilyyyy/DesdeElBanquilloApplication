
using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Competition), "Competition")]
    public partial class CompetitionInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Competition competition;

        partial void OnCompetitionChanged(Competition value)
        {
            Title = value?.Name ?? "Información de la Competición";
        }
    }
}