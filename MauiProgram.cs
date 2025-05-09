using ITC2Wedstrijd.Data.API;
using ITC2Wedstrijd.Data.IRepository;
using ITC2Wedstrijd.Data.Repository;
using Microsoft.Extensions.Logging;

namespace ITC2Wedstrijd
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<APIControllerTeams>();
            builder.Services.AddSingleton<CategorieRepository>();
            builder.Services.AddSingleton<ClubRepository>();
            builder.Services.AddSingleton<SportRepository>();
            builder.Services.AddSingleton<SpelerRepository>();
            builder.Services.AddSingleton<PloegRepository>();
            builder.Services.AddSingleton<SpelerPloegRepository>();
            builder.Services.AddSingleton<WedstrijdenRepository>();
            builder.Services.AddTransient<OverzichtPloegenRepository>();
            builder.Services.AddTransient<OverzichtSpelersRepository>();
            builder.Services.AddTransient<OverzichtWedstrijdenRepository>();

            builder.Services.AddTransient<CategoriePage>();
            builder.Services.AddTransient<ClubPage>();
            builder.Services.AddTransient<SportPage>();
            builder.Services.AddTransient<SpelerPage>();
            builder.Services.AddTransient<PloegPage>();
            builder.Services.AddTransient<ToewijzenPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddSingleton<WedstrijdenPage>();
            builder.Services.AddSingleton<TeamsPage>();
            builder.Services.AddTransient<OverzichtPloegenPage>();
            builder.Services.AddTransient<OverzichtSpelersPage>();
            builder.Services.AddTransient<OverzichtWedstrijdenPage>();

            builder.Services.AddSingleton<CategorieViewModel>();
            builder.Services.AddSingleton<ClubViewModel>();
            builder.Services.AddSingleton<SportViewModel>();
            builder.Services.AddSingleton<SpelerViewModel>();
            builder.Services.AddSingleton<PloegViewModel>();
            builder.Services.AddTransient<ToewijzenViewModel>();
            builder.Services.AddTransient<BaseViewModel>();
            builder.Services.AddSingleton<WedstrijdenViewModel>();
            builder.Services.AddSingleton<TeamsViewModel>();
            builder.Services.AddTransient<OverzichtPloegenViewModel>();
            builder.Services.AddTransient<OverzichtSpelersViewModel>();
            builder.Services.AddTransient<OverzichtWedstrijdenViewModel>();

            return builder.Build();
        }
    }
}
