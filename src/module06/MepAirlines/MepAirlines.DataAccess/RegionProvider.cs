using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace MepAirlines.DataAccess
{
    public interface IRegionProvider
    {
        RegionInfo GetRegionInfoByEnlgishName(string englishName);
    }

    public sealed class RegionProvider : IRegionProvider
    {
        private readonly IOptions<FuckedUpCountriesOption> _fuckUpCountriesOption;
        private readonly Lazy<IReadOnlyDictionary<string, RegionInfo>> _regionsByEnglishName;

        public RegionProvider(IOptions<FuckedUpCountriesOption> fuckUpCountriesOption)
        {
            _fuckUpCountriesOption = fuckUpCountriesOption;
            _regionsByEnglishName = new Lazy<IReadOnlyDictionary<string,RegionInfo>>(GetRegionsByEnglishName);
        }

        private IReadOnlyDictionary<string, RegionInfo> GetRegionsByEnglishName =>
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(a => new RegionInfo(a.Name))
                .Distinct()
                .OrderBy(o => o.EnglishName)
                .ToDictionary(kvp => kvp.EnglishName, kvp => kvp,
                    StringComparer.Create(CultureInfo.InvariantCulture,
                        CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase));

        public RegionInfo GetRegionInfoByEnlgishName(string englishName) =>
            _regionsByEnglishName.Value[
                _fuckUpCountriesOption.Value.FuckedUpCountries.TryGetValue(englishName, out var reterdlessName)
                    ? reterdlessName
                    : englishName];
    }
}
