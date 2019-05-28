using MepAirlines.DataAccess.Entities;

namespace MepAirlines.DataAccess.EntityBuilders
{
    public interface ICountryBuilder
    {
        Country CreateCountry(DatRecord datRecord, int id);
    }

    public sealed class CountryBuilder : ICountryBuilder
    {
        private readonly IRegionProvider _regionProvider;

        public CountryBuilder(IRegionProvider regionProvider)
        {
            _regionProvider = regionProvider;
        }

        public Country CreateCountry(DatRecord datRecord, int id)
        {
            var regionInfo = _regionProvider.GetRegionInfoByEnlgishName(datRecord.Country);

            return new Country
            {
                Id = id,
                Name = datRecord.Country,
                ThreeLetterIsoCode = regionInfo.ThreeLetterISORegionName,
                TwoLetterIsoCode = regionInfo.TwoLetterISORegionName
            };
        }
    }
}
