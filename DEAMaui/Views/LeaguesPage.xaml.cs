namespace DEAMaui.Views;
public partial class LeaguesPage : ContentPage
{
    private readonly ViewModels.LeaguesViewModel _viewModel;
    public LeaguesPage(ViewModels.LeaguesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetLeaguesCommand.Execute(null);
    }
}