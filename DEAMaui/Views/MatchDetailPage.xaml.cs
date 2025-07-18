namespace DEAMaui.Views;
public partial class MatchDetailPage : ContentPage
{
    private readonly ViewModels.MatchDetailViewModel _viewModel;
    public MatchDetailPage(ViewModels.MatchDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDependenciesAsync();
    }
}