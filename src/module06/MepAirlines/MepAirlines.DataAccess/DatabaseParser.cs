using System.Collections.Generic;
using System.Linq;
using MepAirlines.DataAccess.Entities;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace MepAirlines.DataAccess
{
    public interface IDatabaseParser
    {
        bool TryParseDatabase(out Database database);
    }

    public sealed class DatabaseParser : IDatabaseParser
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IFileService _fileService;
        private readonly IOptions<FileOption> _fileOptions;

        public DatabaseParser(
            IJsonSerializer jsonSerializer,
            IFileService fileService,
            IOptions<FileOption> fileOptions)
        {
            _jsonSerializer = jsonSerializer;
            _fileService = fileService;
            _fileOptions = fileOptions;
        }

        public bool TryParseDatabase(out Database database)
        {
            database = null;

            if (_fileService.Exists(_fileOptions.Value.CountriesFileName) &&
                _fileService.Exists(_fileOptions.Value.CitiesFileName) &&
                _fileService.Exists(_fileOptions.Value.AirportsFileName))
            {
                try
                {

                    var countries = _jsonSerializer.Deserialize<List<Country>>(
                            _fileService.ReadAllText(_fileOptions.Value.CountriesFileName))
                        .ToDictionary(kvp => kvp.Id, kvp => kvp);

                    var cities = _jsonSerializer.Deserialize<List<City>>(
                            _fileService.ReadAllText(_fileOptions.Value.CitiesFileName))
                        .ToDictionary(kvp => kvp.Id, kvp =>
                        {
                            kvp.Country = countries[kvp.CountryId];

                            return kvp;
                        });

                    

                    var airports = _jsonSerializer.Deserialize<List<Airport>>(
                        _fileService.ReadAllText(_fileOptions.Value.AirportsFileName)).Select(s =>
                    {
                        s.City = cities[s.CityId];
                        s.Country = s.City.Country;

                        return s;
                    }).ToList();

                    database = new Database
                    {
                        Countries = countries.Values,
                        Cities = cities.Values,
                        Airports = airports
                    };

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
