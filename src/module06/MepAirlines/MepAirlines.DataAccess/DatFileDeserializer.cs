using System.Collections.Generic;
using System.Globalization;
using MepAirlines.DataAccess.Entities;
using MepAirlines.DataAccess.Extensions;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MepAirlines.DataAccess
{
    public interface IDatFileDeserializer
    {
        IEnumerable<DatRecord> GetValidDatRecords();
    }

    public sealed class DatFileDeserializer : IDatFileDeserializer
    {
        private readonly IOptions<FileOption> _fileOptions;
        private readonly ILogger<DatFileDeserializer> _logger;
        private readonly IFileService _fileService;
        private readonly IDatValidator _datValidator;

        public DatFileDeserializer(
            IOptions<FileOption> fileOptions,
            ILogger<DatFileDeserializer> logger,
            IFileService fileService,
            IDatValidator datValidator)
        {
            _fileOptions = fileOptions;
            _logger = logger;
            _fileService = fileService;
            _datValidator = datValidator;
        }

        public IEnumerable<DatRecord> GetValidDatRecords()
        {
            var numberOfInvalidRecords = 0;

            foreach (var line in _fileService.ReadAllLines(_fileOptions.Value.DatFileName))
            {
                if (_datValidator.IsValid(line))
                {
                    var rawData = line.Split(',');

                    yield return new DatRecord
                    {
                        Id = int.Parse(rawData[0].Trim(), CultureInfo.InvariantCulture),
                        Airport = rawData[1].Trim().RemoveQuote(),
                        City = rawData[2].Trim().RemoveQuote(),
                        Country = rawData[3].Trim().RemoveQuote(),
                        Iata = rawData[4].Trim().RemoveQuote(),
                        Icao = rawData[5].Trim().RemoveQuote(),
                        Latitude = float.Parse(rawData[6].Trim(), CultureInfo.InvariantCulture),
                        Longitutde = float.Parse(rawData[7].Trim(), CultureInfo.InvariantCulture),
                        Altitude = int.Parse(rawData[8].Trim(), CultureInfo.InvariantCulture),
                    };
                }
                else
                {
                    _logger.LogTrace($"The line was incorret: {line}");
                    numberOfInvalidRecords++;
                }
            }

            _logger.LogInformation($"Total number of invalid lines: {numberOfInvalidRecords}");
        }
    }
}