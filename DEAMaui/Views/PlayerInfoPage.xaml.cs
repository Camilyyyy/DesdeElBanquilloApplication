namespace DEAMaui.Views;
public partial class PlayerInfoPage : ContentPage
{
    public PlayerInfoPage(ViewModels.PlayerInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}