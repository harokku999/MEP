using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MepAirlines.BusinessLogic;

namespace MepAirlines.ConsoleUi
{
    public interface IApplication
    {
        void Run();
    }

    public sealed class Application : IApplication
    {
        private readonly IReportService _reportService;
        private readonly IUserInputService _userInputService;
        private readonly IDateTimeService _dateTimeService;

        public Application(
            IReportService reportService,
            IUserInputService userInputService,
            IDateTimeService dateTimeService)
        {
            _reportService = reportService;
            _userInputService = userInputService;
            _dateTimeService = dateTimeService;
        }

        public void Run()
        {
            WriteReport(
                "List all the countries by name in an ascending order, and display the number of airports they have. ",
                _reportService.GetAllCountryWithAirportsCount);


             WriteReport(
                 "Find the city which has got the most airports. If there are more than one cities with the same amount, display all of them.",
                 _reportService.GetCitiesWithTheMostAirport);

            ClosestAirport();
            GetAirportByIata();

            Console.ReadKey();

        }

        private void ClosestAirport()
        {
            Console.WriteLine("Find the closest airport...");

            bool LongLatValidator(string str) =>
                !string.IsNullOrEmpty(str) && float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture,
                                               out var value)
                                           && value >= -180 && value <= 180;

            float LongLatParser(string str) => float.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);

            var latitude = _userInputService.GetDataFromUser("Gimme a latitude: ", LongLatValidator, LongLatParser,
                "It's not a valid latitude :/ Try again...");

            var longitude = _userInputService.GetDataFromUser("Gimme a longitude: ", LongLatValidator, LongLatParser,
                "It's not a valid latitude :/ Try again...");

            var closestAirport = _reportService.GetTheClosestAirport(latitude, longitude);

            Console.WriteLine($"The closest airport is: {closestAirport}");
        }

        private void GetAirportByIata()
        {
            Console.WriteLine("Find airport by IATA...");

            var iata = _userInputService.GetDataFromUser("Gimme an IATA: ",
                str => !string.IsNullOrEmpty(str) && str.Trim().Length == 3 && str.Trim().All(char.IsLetter),
                str => str.Trim().ToUpperInvariant(), "It's not a valid IATA :/ Try again...");

            while (true)
            {
                var found = _reportService.TryGetAirportByIataCode(iata, out var airport);

                if (found)
                {
                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(airport.TimeZoneName);

                    Console.WriteLine(
                        $"Found! {airport.Name} - {airport.Country.Name}, {airport.Country.Name} - {airport.TimeZoneName} - Local time: {TimeZoneInfo.ConvertTime(_dateTimeService.UtcNow(), timeZone)}");
                    return;
                }

                else
                {
                    Console.WriteLine("Not found :( Try again...");
                }
            }
        }

        private void WriteReport(string title, Func<IEnumerable<string>> report)
        {
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
            foreach (var line in report())
            {
                Console.WriteLine(line);
            }

        }
    }
}
