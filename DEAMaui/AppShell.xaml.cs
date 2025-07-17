using DEAMaui.Views;
namespace DEAMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
            Routing.RegisterRoute(nameof(CountryInfoPage), typeof(CountryInfoPage));
        }
    }
}
