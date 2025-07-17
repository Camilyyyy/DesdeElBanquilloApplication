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

            return builder.Build();
        }
    }
}
