namespace DEAMaui.Views;
public partial class FederationInfoPage : ContentPage
{
    public FederationInfoPage(ViewModels.FederationInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}