using System;

namespace MepAirlines.DataAccess
{
    public interface IDatabaseFactory
    {
        IDatabase GetDatabase();
    }

    public sealed class DatabaseFactory : IDatabaseFactory
    {
        private readonly IDatabaseGenerator _databaseGenerator;
        private readonly IDatabasePersister _databasePersister;
        private readonly IDatabaseParser _databaseParser;

        private readonly Lazy<IDatabase> _database;

        public IDatabase GetDatabase() => _database.Value;

        public DatabaseFactory(
            IDatabaseGenerator databaseGenerator,
            IDatabasePersister databasePersister,
            IDatabaseParser databaseParser)
        {
            _databaseGenerator = databaseGenerator;
            _databasePersister = databasePersister;
            _databaseParser = databaseParser;
            _database = new Lazy<IDatabase>(CreateDatabase);
        }

        internal IDatabase CreateDatabase()
        {
            if (_databaseParser.TryParseDatabase(out var databaseFromFile))
            {
                return databaseFromFile;
            }

            var database = _databaseGenerator.GenerateDatabase();
            _databasePersister.SaveDatabase(database);

            return database;
        }
    }
}