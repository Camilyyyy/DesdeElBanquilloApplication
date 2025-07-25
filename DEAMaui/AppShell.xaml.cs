﻿using DEAMaui.Views;
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

            //Jugador
            Routing.RegisterRoute(nameof(PlayerDetailPage), typeof(PlayerDetailPage));
            Routing.RegisterRoute(nameof(PlayerInfoPage), typeof(PlayerInfoPage));

            //Equipo
            Routing.RegisterRoute(nameof(TeamDetailPage), typeof(TeamDetailPage));
            Routing.RegisterRoute(nameof(TeamInfoPage), typeof(TeamInfoPage));

            // Competition
            Routing.RegisterRoute(nameof(CompetitionDetailPage), typeof(CompetitionDetailPage));
            Routing.RegisterRoute(nameof(CompetitionInfoPage), typeof(CompetitionInfoPage));

            //Season
            Routing.RegisterRoute(nameof(SeasonDetailPage), typeof(SeasonDetailPage));
            Routing.RegisterRoute(nameof(SeasonInfoPage), typeof(SeasonInfoPage));

            //Stadiums
            Routing.RegisterRoute(nameof(StadiumDetailPage), typeof(StadiumDetailPage));
            Routing.RegisterRoute(nameof(StadiumInfoPage), typeof(StadiumInfoPage));
            //Matches
            Routing.RegisterRoute(nameof(MatchDetailPage), typeof(MatchDetailPage));
            Routing.RegisterRoute(nameof(MatchInfoPage), typeof(MatchInfoPage));
        }
    }
}
