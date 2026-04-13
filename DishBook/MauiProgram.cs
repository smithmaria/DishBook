using DishBook.Pages;
using DishBook.Services;
using Microsoft.Extensions.Logging;

namespace DishBook
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

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<ViewPage>();
            builder.Services.AddTransient<AddPage>();
            builder.Services.AddTransient<FavoritesPage>();
            builder.Services.AddTransient<RecipeDetailPage>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
