using AppStore.Common.Domain.Persistence.MongoDB.Base;

namespace AppStore.Common.Infrastructure.Persistance.MongoDB.Base
{
    public sealed record MongoSettings : IMongoSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
