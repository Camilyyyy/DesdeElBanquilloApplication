namespace DEAMaui.Views;
public partial class PositionsPage : ContentPage
{
    private readonly ViewModels.PositionsViewModel _viewModel;
    public PositionsPage(ViewModels.PositionsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetPositionsCommand.Execute(null);
    }
}