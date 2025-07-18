namespace DEAMaui.Views;
public partial class PlayerDetailPage : ContentPage
{
    private readonly ViewModels.PlayerDetailViewModel _viewModel;
    public PlayerDetailPage(ViewModels.PlayerDetailViewModel viewModel)
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