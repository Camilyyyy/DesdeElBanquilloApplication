namespace DEAMaui.Views;
public partial class StadiumsPage : ContentPage
{
    private readonly ViewModels.StadiumsViewModel _viewModel;
    public StadiumsPage(ViewModels.StadiumsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetStadiumsCommand.Execute(null);
    }
}