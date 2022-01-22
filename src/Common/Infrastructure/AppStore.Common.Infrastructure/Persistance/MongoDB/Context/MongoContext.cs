using AppStore.Common.Domain.Persistence.MongoDB;
using AppStore.Common.Domain.Persistence.MongoDB.Base;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.Persistance.MongoDB.Context
{
    public sealed class MongoContext : IMongoContext
    {
        #region Fields

        private MongoClient mongoClient { get; set; }
        private IClientSessionHandle session { get; set; }
        private IMongoDatabase database { get; set; }

        private readonly List<Func<Task>> commands;
        private readonly IMongoSettings mongoSettings;

        #endregion

        #region Constructor

        public MongoContext(IMongoSettings mongoSettings)
        {
            this.mongoSettings = mongoSettings ?? throw new ArgumentNullException(nameof(mongoSettings));

            commands = new List<Func<Task>>();
        }

        #endregion

        #region IMongoContext Methods

        public void AddCommand(Func<Task> command)
            => commands.Add(command);

        public async Task<int> SaveChangesAsync()
        {
            SetupMongo();

            using (session = await mongoClient.StartSessionAsync())
            {
                session.StartTransaction();

                var commandsTasks = commands.Select(_ => _());

                await Task.WhenAll(commandsTasks);

                await session.CommitTransactionAsync();
            }

            return commands.Count;
        }

        public IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName)
        {
            SetupMongo();

            return database.GetCollection<TCollection>(collectionName);
        }

        public void Dispose()
        {
            commands?.Clear();

            session?.Dispose();

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Methods

        private void SetupMongo()
        {
            if (mongoClient != null)
                return;

            mongoClient = new MongoClient(mongoSettings.ConnectionString);

            database = mongoClient.GetDatabase(mongoSettings.DatabaseName);
        }

        #endregion
    }
}