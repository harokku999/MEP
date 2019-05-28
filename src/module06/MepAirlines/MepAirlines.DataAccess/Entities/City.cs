using Newtonsoft.Json;

namespace MepAirlines.DataAccess.Entities
{
    public sealed class City
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
