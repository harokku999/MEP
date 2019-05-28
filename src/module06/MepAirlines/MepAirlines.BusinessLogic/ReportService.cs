using System.Collections.Generic;
using System.Linq;
using MepAirlines.DataAccess;
using MepAirlines.DataAccess.Entities;

namespace MepAirlines.BusinessLogic
{
    public interface IReportService
    {
        IEnumerable<string> GetAllCountryWithAirportsCount();
        IEnumerable<string> GetCitiesWithTheMostAirport();
        string GetTheClosestAirport(float latitude, float longitude);
        bool TryGetAirportByIataCode(string iataCode, out Airport airport);
    }

    public sealed class ReportService : IReportService
    {
        private readonly IDistanceCalculator _distanceCalculator;
        private readonly IDatabase _database;

        public ReportService(
            IDatabaseFactory databaseFactory,
            IDistanceCalculator distanceCalculator)
        {
            _distanceCalculator = distanceCalculator;
            _database = databaseFactory.GetDatabase();
        }

        public IEnumerable<string> GetAllCountryWithAirportsCount()
        {
            var query = _database.Airports.GroupBy(a => a.Country).Select(a => new
            {
                Country = a.Key.Name,
                Airports = a.Count()
            }).OrderBy(a => a.Country).Select(a => $"{a.Country}: {a.Airports}");

            return query.ToList();
        }

        public IEnumerable<string> GetCitiesWithTheMostAirport()
        {
            var query = _database.Airports.GroupBy(a => a.City).Select(a => new
            {
                City = a.Key.Name,
                Airports = a.Count()
            }).OrderByDescending(a => a.Airports).ToList();

            var top = query.First().Airports;
            var allTheTops = query.TakeWhile(a => a.Airports == top).Select(a => a.City);

            return allTheTops.ToList();
        }

        public string GetTheClosestAirport(float latitude, float longitude)
        {
            var closestAirport = _database.Airports.Select(a => new
            {
                Airport = a,
                Distance = _distanceCalculator.GetDistanceInKm(latitude, longitude, a.Location.Latitude,
                    a.Location.Longitude)
            }).OrderBy(a => a.Distance).First();

            return
                $"{closestAirport.Airport.Name} - {closestAirport.Airport.City.Name} - {closestAirport.Airport.Country.Name}";
        }

        public bool TryGetAirportByIataCode(string iataCode, out Airport airport)
        {
            

            airport = _database.Airports.SingleOrDefault(a => a.IataCode.ToUpperInvariant() ==
                                                    iataCode.Trim().ToUpperInvariant());

            return airport != null;

        }
    }
}
