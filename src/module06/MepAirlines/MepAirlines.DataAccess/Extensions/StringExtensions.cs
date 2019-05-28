namespace MepAirlines.DataAccess.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveQuote(this string input) => input.Substring(1, input.Length - 2);
    }
}
