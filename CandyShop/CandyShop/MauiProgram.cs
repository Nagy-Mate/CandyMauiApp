namespace CandyShop;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //Views
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<EditPage>();
        builder.Services.AddTransient<SellCandyPage>();
        builder.Services.AddTransient<ReportPage>();

        //View Models
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<EditPageViewModel>();
        builder.Services.AddTransient<SellCandyViewModel>();
        builder.Services.AddTransient<ReportViewModel>();

        //Services
        builder.Services.AddSingleton<FileService>();
        


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
