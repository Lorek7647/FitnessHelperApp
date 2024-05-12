using FitnessHelper.Code;
using Microsoft.Extensions.Logging;

namespace FitnessHelper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            //first entry point
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //maybe not necessary but it works
            builder.Services.AddSingleton<LocalDbService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<ApiServiceMeal>();
            builder.Services.AddSingleton<FitnessHelper.Pages.Weight>();
            builder.Services.AddSingleton<FitnessHelper.Pages.Timer>();
            builder.Services.AddSingleton<FitnessHelper.Pages.DailyMeal>();
            builder.Services.AddSingleton<FitnessHelper.Pages.DailyQuote>();
            builder.Services.AddSingleton<FitnessHelper.Pages.YOU>();
            return builder.Build();
        }
    }
}
