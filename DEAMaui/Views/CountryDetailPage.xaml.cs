namespace DEAMaui.Views;

public partial class CountryDetailPage : ContentPage
{

    public CountryDetailPage(ViewModels.CountryDetailViewModel viewModel)
    {
        InitializeComponent();


        BindingContext = viewModel;
    }
}