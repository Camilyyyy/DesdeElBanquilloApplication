using DEAMaui.Views;
namespace DEAMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Pais
            Routing.RegisterRoute(nameof(CountryDetailPage), typeof(CountryDetailPage));
            Routing.RegisterRoute(nameof(CountryInfoPage), typeof(CountryInfoPage));
            //Posicion
            Routing.RegisterRoute(nameof(PositionDetailPage), typeof(PositionDetailPage));
            Routing.RegisterRoute(nameof(PositionInfoPage), typeof(PositionInfoPage));
        }
    }
}
