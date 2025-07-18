namespace DEAMaui.Views;
public partial class PlayersPage : ContentPage
{
    private readonly ViewModels.PlayersViewModel _viewModel;
    public PlayersPage(ViewModels.PlayersViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetPlayersCommand.Execute(null);
    }
}