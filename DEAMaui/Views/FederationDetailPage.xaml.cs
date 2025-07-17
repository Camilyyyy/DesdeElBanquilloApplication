namespace DEAMaui.Views;
public partial class FederationDetailPage : ContentPage
{
    private readonly ViewModels.FederationDetailViewModel _viewModel;
    public FederationDetailPage(ViewModels.FederationDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Llamamos al m�todo para cargar los pa�ses para el Picker
        await _viewModel.LoadCountriesAsync();
    }
}