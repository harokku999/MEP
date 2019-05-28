namespace MepAirlines.DataAccess.Entities
{
    public class DatRecord
    {
        public int Id { get; set; }
        public string Airport { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public float Longitutde { get; set; }
        public float Latitude { get; set; }
        public float Altitude { get; set; }
    }
}