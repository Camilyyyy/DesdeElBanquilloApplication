using DEAMaui.Views;
using DEAMaui.ViewModels;
using Microsoft.Extensions.Logging;
using DEAMaui.Services;
namespace DEAMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IApiService, ApiService>();

            // Home

            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<HomePage>();

            //Paises

            builder.Services.AddSingleton<CountriesViewModel>();
            builder.Services.AddSingleton<CountriesPage>();
            builder.Services.AddTransient<CountryDetailViewModel>();
            builder.Services.AddTransient<CountryDetailPage>();
            builder.Services.AddTransient<CountryInfoViewModel>();
            builder.Services.AddTransient<CountryInfoPage>();

            //Posicion

            builder.Services.AddSingleton<PositionsViewModel>();
            builder.Services.AddSingleton<PositionsPage>();
            builder.Services.AddTransient<PositionDetailViewModel>();
            builder.Services.AddTransient<PositionDetailPage>();
            builder.Services.AddTransient<PositionInfoViewModel>();
            builder.Services.AddTransient<PositionInfoPage>();

            //Federation

            builder.Services.AddSingleton<FederationsViewModel>();
            builder.Services.AddSingleton<FederationsPage>();
            builder.Services.AddTransient<FederationDetailViewModel>();
            builder.Services.AddTransient<FederationDetailPage>();
            builder.Services.AddTransient<FederationInfoViewModel>();
            builder.Services.AddTransient<FederationInfoPage>();

            //Ligas

            builder.Services.AddSingleton<LeaguesViewModel>();
            builder.Services.AddSingleton<LeaguesPage>();
            builder.Services.AddTransient<LeagueDetailViewModel>();
            builder.Services.AddTransient<LeagueDetailPage>();
            builder.Services.AddTransient<LeagueInfoViewModel>();
            builder.Services.AddTransient<LeagueInfoPage>();

            //Jugador
            builder.Services.AddSingleton<PlayersViewModel>();
            builder.Services.AddSingleton<PlayersPage>();
            builder.Services.AddTransient<PlayerDetailViewModel>();
            builder.Services.AddTransient<PlayerDetailPage>();
            builder.Services.AddTransient<PlayerInfoViewModel>();
            builder.Services.AddTransient<PlayerInfoPage>();

            //Equipo

            builder.Services.AddSingleton<TeamsViewModel>();
            builder.Services.AddSingleton<TeamsPage>();
            builder.Services.AddTransient<TeamDetailViewModel>();
            builder.Services.AddTransient<TeamDetailPage>();
            builder.Services.AddTransient<TeamInfoViewModel>();
            builder.Services.AddTransient<TeamInfoPage>();

            //Competitions

            builder.Services.AddSingleton<CompetitionsViewModel>();
            builder.Services.AddSingleton<CompetitionsPage>();
            builder.Services.AddTransient<CompetitionDetailViewModel>();
            builder.Services.AddTransient<CompetitionDetailPage>();
            builder.Services.AddTransient<CompetitionInfoViewModel>();
            builder.Services.AddTransient<CompetitionInfoPage>();

            //Season

            builder.Services.AddSingleton<SeasonsViewModel>();
            builder.Services.AddSingleton<SeasonsPage>();
            builder.Services.AddTransient<SeasonDetailViewModel>();
            builder.Services.AddTransient<SeasonDetailPage>();
            builder.Services.AddTransient<SeasonInfoViewModel>();
            builder.Services.AddTransient<SeasonInfoPage>();

            //Stadiums

            builder.Services.AddSingleton<StadiumsViewModel>();
            builder.Services.AddSingleton<StadiumsPage>();
            builder.Services.AddTransient<StadiumDetailViewModel>();
            builder.Services.AddTransient<StadiumDetailPage>();
            builder.Services.AddTransient<StadiumInfoViewModel>();
            builder.Services.AddTransient<StadiumInfoPage>();

            //Matches
            builder.Services.AddSingleton<MatchesViewModel>();
            builder.Services.AddSingleton<MatchesPage>();
            builder.Services.AddTransient<MatchDetailViewModel>();
            builder.Services.AddTransient<MatchDetailPage>();
            builder.Services.AddTransient<MatchInfoViewModel>();
            builder.Services.AddTransient<MatchInfoPage>();

            return builder.Build();
        }
    }
}
