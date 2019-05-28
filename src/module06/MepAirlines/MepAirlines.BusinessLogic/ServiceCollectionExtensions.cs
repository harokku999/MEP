using Microsoft.Extensions.DependencyInjection;

namespace MepAirlines.BusinessLogic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseReports(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IReportService, ReportService>();
            serviceCollection.AddSingleton<IDistanceCalculator, DistanceCalculator>();
            return serviceCollection;
        }
    }
}
