namespace DEAMaui.Views;

public partial class HomePage : ContentPage
{
    public HomePage(ViewModels.HomePageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}