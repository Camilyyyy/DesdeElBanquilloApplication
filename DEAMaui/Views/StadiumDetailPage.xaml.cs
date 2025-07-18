namespace DEAMaui.Views;
public partial class StadiumDetailPage : ContentPage
{
    private readonly ViewModels.StadiumDetailViewModel _viewModel;
    public StadiumDetailPage(ViewModels.StadiumDetailViewModel viewModel)
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