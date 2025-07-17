namespace DEAMaui.Views;

public partial class CountryInfoPage : ContentPage
{
	public CountryInfoPage(ViewModels.CountryInfoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}