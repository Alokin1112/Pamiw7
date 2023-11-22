using Microsoft.Extensions.Logging;
using Pamiw6.Shared.Configuration;
using Pamiw6.Shared.Services.AuthorService;
using Pamiw6.Shared.Services.BookService;
using Pamiw7.MessageBox;
using Pamiw7.shared.interfaces;
using Pamiw7.ViewModels;

namespace Pamiw7
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
            ConfigureServices(builder.Services);
            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = ConfigureAppSettings(services);
            ConfigureAppServices(services);
            ConfigureViewModels(services);
            ConfigureViews(services);
        }

        private static AppSettings ConfigureAppSettings(IServiceCollection services)
        {
            var appSettingsSection = new AppSettings()
            {
                BaseAPIUrl = "http://localhost:8080",
                BookEndpoint = new BookEndpoint()
                {
                    AllBooksEndpoint = "/api/v1/books",
                    SpecificBookEndpoint = "/api/v1/books/",
                },
                AuthorEndpoint = new AuthorEndpoint()
                {
                    AllAuthorsEndpoint = "/api/v1/authors",
                    SpecificAuthorEndpoint = "/api/v1/authors/"
                }
            };
            services.AddSingleton(appSettingsSection);

            return appSettingsSection;
        }

        private static void ConfigureAppServices(IServiceCollection services)
        {
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<IMessageDialogService, MauiMessageDialogService>();
            services.AddSingleton<AuthorService>();
            services.AddSingleton<BookService>();

            services.AddHttpClient<AuthorService,AuthorService>();
            services.AddHttpClient<BookService, BookService>();
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<BookViewModel>();
            services.AddTransient<BookDetailsViewModel>();
        }


        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddTransient<BookDetailsPage>();
        }

    }
}
