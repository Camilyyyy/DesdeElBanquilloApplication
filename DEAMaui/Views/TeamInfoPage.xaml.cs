namespace DEAMaui.Views;
public partial class TeamInfoPage : ContentPage
{
    public TeamInfoPage(ViewModels.TeamInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}