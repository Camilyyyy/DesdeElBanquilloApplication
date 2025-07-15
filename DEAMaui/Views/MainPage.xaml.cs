using DesdeElBanquillo.ViewModels;

namespace DesdeElBanquillo.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}