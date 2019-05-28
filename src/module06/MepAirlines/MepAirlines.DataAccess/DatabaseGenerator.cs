using System.Collections.Generic;
using System.Linq;
using MepAirlines.DataAccess.Entities;
using MepAirlines.DataAccess.EntityBuilders;

namespace MepAirlines.DataAccess
{
    public interface IDatabaseGenerator
    {
        IDatabase GenerateDatabase();
    }

    public sealed class DatabaseGenerator : IDatabaseGenerator
    {
        private readonly IDatFileDeserializer _datFileDeserializer;
        private readonly ICountryBuilder _countryBuilder;
        private readonly ICityBuilder _cityBuilder;
        private readonly IAirportBuilder _airportBuilder;

        private readonly IDictionary<string, Country> _countryCache;
        private readonly IDictionary<string, City> _cityCache;


        public DatabaseGenerator(
            IDatFileDeserializer datFileDeserializer,
            ICountryBuilder countryBuilder,
            ICityBuilder cityBuilder,
            IAirportBuilder airportBuilder)
        {
            _datFileDeserializer = datFileDeserializer;
            _countryBuilder = countryBuilder;
            _cityBuilder = cityBuilder;
            _airportBuilder = airportBuilder;

            _countryCache = new Dictionary<string, Country>();
            _cityCache = new Dictionary<string, City>();
        }

        public IDatabase GenerateDatabase()
        {
            _countryCache.Clear();
            _cityCache.Clear();

            var records = _datFileDeserializer.GetValidDatRecords();

            var airports = records.Select(record => _airportBuilder.CreateAirport(record, GetCachedCity(record))).ToList();
            var countries = _countryCache.Values;
            var cities = _cityCache.Values;

            return new Database
            {
                Countries = countries,
                Cities = cities,
                Airports = airports
            };
        }

        private City GetCachedCity(DatRecord record)
        {
            if (_cityCache.TryGetValue(record.City, out var cachedCity))
            {
                return cachedCity;
            }

            var newCity = _cityBuilder.CreateCity(record, GetCachedCountry(record), _cityCache.Count + 1);
            _cityCache[record.City] = newCity;

            return newCity;
        }

        private Country GetCachedCountry(DatRecord record)
        {
            if (_countryCache.TryGetValue(record.Country, out var cachedCountry))
            {
                return cachedCountry;
            }

            var newCountry = _countryBuilder.CreateCountry(record, _countryCache.Count + 1);
            _countryCache[record.Country] = newCountry;

            return newCountry;
        }
    }
}