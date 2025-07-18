namespace DEAMaui.Views;
public partial class MatchInfoPage : ContentPage
{
    public MatchInfoPage(ViewModels.MatchInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}