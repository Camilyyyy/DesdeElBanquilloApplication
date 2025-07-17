using DEAMaui.Views.Country;
namespace DEAMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
        }
    }
}
