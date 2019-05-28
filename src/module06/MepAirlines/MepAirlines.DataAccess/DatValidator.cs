using System.Text.RegularExpressions;

namespace MepAirlines.DataAccess
{
    public interface IDatValidator
    {
        bool IsValid(string line);
    }

    public sealed class DatValidator : IDatValidator
    {
        private readonly Regex _validatorRegex;

        public DatValidator()
        {
            _validatorRegex = new Regex(
                "\\d\\s*(,\\s*\"[\\w ]+\"){3}\\s*,\\s*\"[A-Z]{3}\"\\s*,\\s*\"[A-Z]{4}\"\\s*(,\\s*[-+]?[0-9]*\\.[0-9]*\\s*){2},\\s*[\\d]+");
        }

        public bool IsValid(string line) => _validatorRegex.IsMatch(line);
    }
}
