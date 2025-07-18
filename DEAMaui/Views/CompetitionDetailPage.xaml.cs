namespace DEAMaui.Views;
public partial class CompetitionDetailPage : ContentPage
{
    private readonly ViewModels.CompetitionDetailViewModel _viewModel;
    public CompetitionDetailPage(ViewModels.CompetitionDetailViewModel viewModel)
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