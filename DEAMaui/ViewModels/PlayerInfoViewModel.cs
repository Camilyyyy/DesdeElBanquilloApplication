using CommunityToolkit.Mvvm.ComponentModel;
using DEAModels;

namespace DEAMaui.ViewModels
{
    [QueryProperty(nameof(Player), "Player")]
    public partial class PlayerInfoViewModel : BaseViewModel
    {
        [ObservableProperty]
        Player player;

        partial void OnPlayerChanged(Player value)
        {
            Title = value?.Name ?? "Información del Jugador";
        }
    }
}