namespace DEAMaui.Views;
public partial class SeasonInfoPage : ContentPage
{
    public SeasonInfoPage(ViewModels.SeasonInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}