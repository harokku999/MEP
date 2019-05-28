using System.Collections.Generic;
using MepAirlines.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace MepAirlines.DataAccess
{
    public interface IDatabasePersister
    {
        void SaveDatabase(IDatabase database);
    }
    public sealed class DatabasePersister : IDatabasePersister
    {
        private readonly IFileService _fileService;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IOptions<FileOption> _fileOption;

        public DatabasePersister(
            IFileService fileService,
            IJsonSerializer jsonSerializer,
            IOptions<FileOption> fileOption)
        {
            _fileService = fileService;
            _jsonSerializer = jsonSerializer;
            _fileOption = fileOption;
        }

        public void SaveDatabase(IDatabase database)
        {
            SerializeAndWrite(database.Cities, _fileOption.Value.CitiesFileName);
            SerializeAndWrite(database.Countries, _fileOption.Value.CountriesFileName);
            SerializeAndWrite(database.Airports, _fileOption.Value.AirportsFileName);
        }

        private void SerializeAndWrite<TEntity>(IEnumerable<TEntity> models, string fileName)
        {
            var json = _jsonSerializer.Serialize(models);
            _fileService.WriteAllText(fileName, json);
        }
    }
}
