namespace AppStore.Common.Domain.Persistence.MongoDB.Base
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
