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

            //Federacion
            Routing.RegisterRoute(nameof(FederationDetailPage), typeof(FederationDetailPage));
            Routing.RegisterRoute(nameof(FederationInfoPage), typeof(FederationInfoPage));
            
            //Liga
            Routing.RegisterRoute(nameof(LeagueDetailPage), typeof(LeagueDetailPage));
            Routing.RegisterRoute(nameof(LeagueInfoPage), typeof(LeagueInfoPage));
        }
    }
}
