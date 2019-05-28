using MepAirlines.DataAccess.EntityBuilders;
using Microsoft.Extensions.DependencyInjection;

namespace MepAirlines.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDatabaseFactory, DatabaseFactory>();
            serviceCollection.AddSingleton<IDatabaseGenerator, DatabaseGenerator>();
            serviceCollection.AddSingleton<IDatabaseParser, DatabaseParser>();
            serviceCollection.AddSingleton<IDatabasePersister, DatabasePersister>();
            serviceCollection.AddSingleton<IDatFileDeserializer, DatFileDeserializer>();
            serviceCollection.AddSingleton<IFileService, FileService>();
            serviceCollection.AddSingleton<IJsonSerializer, JsonSerializer>();
            serviceCollection.AddSingleton<IDatValidator, DatValidator>();
            serviceCollection.AddSingleton<IRegionProvider, RegionProvider>();
            serviceCollection.AddSingleton<ITimeZoneProvider, TimeZoneProvider>();
            serviceCollection.AddSingleton<IAirportBuilder, AirportBuilder>();
            serviceCollection.AddSingleton<ICityBuilder, CityBuilder>();
            serviceCollection.AddSingleton<ICountryBuilder, CountryBuilder>();

            return serviceCollection;
        }
    }
}
