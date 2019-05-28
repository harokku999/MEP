using Newtonsoft.Json;

namespace MepAirlines.DataAccess.Entities
{
    public sealed class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public string FullName { get; set; }
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }
        public string TimeZoneName { get; set; }

        [JsonIgnore]
        public City City { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }

        public Location Location { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
