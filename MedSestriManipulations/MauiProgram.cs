using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MedSestriManipulations
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Ensure the Encoding.RegisterProvider call is inside a method, not at the class level.
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
