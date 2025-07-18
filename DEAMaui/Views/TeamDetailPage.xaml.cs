namespace DEAMaui.Views;
public partial class TeamDetailPage : ContentPage
{
    private readonly ViewModels.TeamDetailViewModel _viewModel;
    public TeamDetailPage(ViewModels.TeamDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDependenciesAsync();
    }
}