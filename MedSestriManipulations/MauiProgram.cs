using CommunityToolkit.Maui;
using MedSestriManipulations.Services;
using MedSestriManipulations.Services.History;
using MedSestriManipulations.Services.SMS;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MedSestriManipulations
{
    public static class MauiProgram
    {
        public static MauiApp AppInstance { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var builder = MauiApp.CreateBuilder();

            builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://medsestribackendapi20250430210231-d9b9grdkdnecc0aw.italynorth-01.azurewebsites.net/");
            });

            builder.Services.AddSingleton<HistoryService>();
            builder.Services.AddSingleton<SmsParserService>();
            builder.Services.AddSingleton<SmsPermissionService>();
            builder.Services.AddSingleton<PaginationState>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            AppInstance = app;
            var historyService = app.Services.GetRequiredService<HistoryService>();
            _ = historyService.ReadAllPatientsFromCloud();
            return app;
        }
    }
}
