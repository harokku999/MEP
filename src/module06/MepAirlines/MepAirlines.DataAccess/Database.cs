using System.Collections.Generic;
using MepAirlines.DataAccess.Entities;

namespace MepAirlines.DataAccess
{
    public interface IDatabase
    {
        IEnumerable<Airport> Airports { get; set; }
        IEnumerable<City> Cities { get; set; }
        IEnumerable<Country> Countries { get; set; }
    }

    public sealed class Database : IDatabase
    {
        public IEnumerable<Airport> Airports { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Country> Countries { get; set; }
    }
}