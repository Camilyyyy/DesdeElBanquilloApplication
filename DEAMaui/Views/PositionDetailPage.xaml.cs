namespace DEAMaui.Views;
public partial class PositionDetailPage : ContentPage
{
    public PositionDetailPage(ViewModels.PositionDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}