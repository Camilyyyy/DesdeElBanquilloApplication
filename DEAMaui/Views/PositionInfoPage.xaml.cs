namespace DEAMaui.Views;
public partial class PositionInfoPage : ContentPage
{
    public PositionInfoPage(ViewModels.PositionInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}