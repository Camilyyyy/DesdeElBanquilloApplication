namespace DEAMaui.Views;
public partial class FederationsPage : ContentPage
{
    private readonly ViewModels.FederationsViewModel _viewModel;
    public FederationsPage(ViewModels.FederationsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetFederationsCommand.Execute(null);
    }
}