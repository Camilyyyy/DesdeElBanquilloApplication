namespace DEAMaui.Views;
public partial class LeagueDetailPage : ContentPage
{
    private readonly ViewModels.LeagueDetailViewModel _viewModel;
    public LeagueDetailPage(ViewModels.LeagueDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCountriesAsync();
    }
}