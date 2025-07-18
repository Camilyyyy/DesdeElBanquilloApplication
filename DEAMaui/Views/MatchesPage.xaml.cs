namespace DEAMaui.Views;
public partial class MatchesPage : ContentPage
{
    private readonly ViewModels.MatchesViewModel _viewModel;
    public MatchesPage(ViewModels.MatchesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetMatchesCommand.Execute(null);
    }
}