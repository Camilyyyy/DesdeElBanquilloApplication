namespace DEAMaui.Views;
public partial class StadiumInfoPage : ContentPage
{
    public StadiumInfoPage(ViewModels.StadiumInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}