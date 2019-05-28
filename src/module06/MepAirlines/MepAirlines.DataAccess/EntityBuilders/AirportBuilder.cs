using MepAirlines.DataAccess.Entities;

namespace MepAirlines.DataAccess.EntityBuilders
{
    public interface IAirportBuilder
    {
        Airport CreateAirport(DatRecord datRecord, City city);
    }

    public sealed class AirportBuilder : IAirportBuilder
    {
        public Airport CreateAirport(DatRecord datRecord, City city)
        {
            return new Airport
            {
                Id = datRecord.Id,
                IataCode = datRecord.Iata,
                IcaoCode = datRecord.Icao,
                Name = datRecord.Airport,
                FullName = $"{datRecord.Airport} Airport",
                City = city,
                CityId = city.Id,
                Country = city.Country,
                TimeZoneName = city.TimeZoneName,

                Location = new Location
                {
                    Longitude = datRecord.Longitutde,
                    Latitude = datRecord.Latitude,
                    Altitude = datRecord.Altitude
                }
            };
        }
    }
}
