namespace DEAMaui.Views;
public partial class SeasonDetailPage : ContentPage
{
    private readonly ViewModels.SeasonDetailViewModel _viewModel;
    public SeasonDetailPage(ViewModels.SeasonDetailViewModel viewModel)
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