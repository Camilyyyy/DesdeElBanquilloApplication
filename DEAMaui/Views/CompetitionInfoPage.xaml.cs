namespace DEAMaui.Views;
public partial class CompetitionInfoPage : ContentPage
{
    public CompetitionInfoPage(ViewModels.CompetitionInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}