namespace DEAMaui.Views;
using DEAMaui.ViewModels;

    public partial class CountriesPage : ContentPage
    {
        public CountriesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Cargar los países automáticamente cuando la página aparece
            (this.BindingContext as CountriesViewModel)?.LoadCountriesCommand.Execute(null);
        }
    }

  