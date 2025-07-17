namespace DEAMaui.Views;

public partial class CountriesPage : ContentPage
{
    // Mantenemos una referencia por si la necesitamos, pero la asignación principal es en el constructor
    private readonly ViewModels.CountriesViewModel _viewModel;

    public CountriesPage(ViewModels.CountriesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    // El OnAppearing sigue siendo importante para la carga inicial de datos.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Llama al comando para cargar los datos automáticamente.
        _viewModel.GetCountriesCommand.Execute(null);
    }

    // Ya no necesitamos los manejadores de eventos OnEditSwipeItemInvoked, etc.
    // El enlace de datos se hace directamente en el XAML del MenuFlyout.
}