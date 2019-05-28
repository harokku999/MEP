using System.IO;
using MepAirlines.BusinessLogic;
using MepAirlines.DataAccess;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace MepAirlines.ConsoleUi
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));


            var serviceCollection = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                        loggingBuilder.AddNLog(configuration);
                    })
                .UseDatabase()
                .UseReports()
                .AddSingleton<IUserInputService, UserInputService>()
                .AddSingleton<IDateTimeService, DateTimeService>();

            serviceCollection.AddSingleton<IApplication, Application>();

            

            serviceCollection.AddOptions();
            serviceCollection.Configure<FuckedUpCountriesOption>(configuration.GetSection(nameof(FuckedUpCountriesOption)));
            serviceCollection.Configure<FileOption>(configuration.GetSection(nameof(FileOption)));

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var application = serviceProvider.GetService<IApplication>();
            application.Run();
        }
    }
}
