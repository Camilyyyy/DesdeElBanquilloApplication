namespace DEAMaui.Views;
public partial class CompetitionsPage : ContentPage
{
    private readonly ViewModels.CompetitionsViewModel _viewModel;
    public CompetitionsPage(ViewModels.CompetitionsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetCompetitionsCommand.Execute(null);
    }
}