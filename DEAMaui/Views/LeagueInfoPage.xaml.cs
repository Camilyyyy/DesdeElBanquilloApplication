namespace DEAMaui.Views;
public partial class LeagueInfoPage : ContentPage
{
    public LeagueInfoPage(ViewModels.LeagueInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}