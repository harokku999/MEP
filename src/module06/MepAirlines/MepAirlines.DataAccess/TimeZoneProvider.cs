using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneInfo = MepAirlines.DataAccess.Entities.TimeZoneInfo;

namespace MepAirlines.DataAccess
{
    public interface ITimeZoneProvider
    {
        string GetTimeZoneNameByAirportId(int airportId);
    }

    public sealed class TimeZoneProvider : ITimeZoneProvider
    {
        public const string TimeZoneJsonFileName = "timezoneinfo.json";

        private readonly IFileService _fileService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly Lazy<IReadOnlyDictionary<int, string>> _airportNameMapping;

        public TimeZoneProvider(
            IFileService fileService,
            IJsonSerializer jsonSerializer)
        {
            _fileService = fileService;
            _jsonSerializer = jsonSerializer;
            _airportNameMapping = new Lazy<IReadOnlyDictionary<int, string>>(CreateAirportNameMapping);
        }


        private IReadOnlyDictionary<int, string> CreateAirportNameMapping()
        {
            var timeZoneFileContent = _fileService.ReadAllText(TimeZoneJsonFileName);
            var timeZoneInfos = _jsonSerializer.Deserialize<List<TimeZoneInfo>>(timeZoneFileContent);

            return timeZoneInfos.ToDictionary(kvp => kvp.AirportId, kvp => kvp.TimeZoneInfoId);
        }

        public string GetTimeZoneNameByAirportId(int airportId) => _airportNameMapping.Value[airportId];
    }
}
