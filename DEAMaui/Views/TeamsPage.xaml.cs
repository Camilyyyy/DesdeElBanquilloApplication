namespace DEAMaui.Views;
public partial class TeamsPage : ContentPage
{
    private readonly ViewModels.TeamsViewModel _viewModel;
    public TeamsPage(ViewModels.TeamsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetTeamsCommand.Execute(null);
    }
}