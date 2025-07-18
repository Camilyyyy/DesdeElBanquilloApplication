namespace DEAMaui.Views;
public partial class SeasonsPage : ContentPage
{
    private readonly ViewModels.SeasonsViewModel _viewModel;
    public SeasonsPage(ViewModels.SeasonsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetSeasonsCommand.Execute(null);
    }
}