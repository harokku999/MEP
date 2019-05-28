using MepAirlines.DataAccess.Entities;

namespace MepAirlines.DataAccess.EntityBuilders
{
    public interface ICityBuilder
    {
        City CreateCity(DatRecord datRecord, Country country, int id);
    }

    public sealed class CityBuilder : ICityBuilder
    {
        private readonly ITimeZoneProvider _timeZoneProvider;

        public CityBuilder(ITimeZoneProvider timeZoneProvider)
        {
            _timeZoneProvider = timeZoneProvider;
        }

        public City CreateCity(DatRecord datRecord, Country country, int id)
        {
            var timeZoneName = _timeZoneProvider.GetTimeZoneNameByAirportId(datRecord.Id);

            return new City
            {
                Id = id,
                Country = country,
                Name = datRecord.City,
                TimeZoneName = timeZoneName,
                CountryId = country.Id
            };
        }
    }
}
