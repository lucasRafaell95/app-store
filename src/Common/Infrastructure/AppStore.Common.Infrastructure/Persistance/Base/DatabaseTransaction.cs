using AppStore.Common.Domain.Persistence.Base;
using AppStore.Common.Domain.Persistence.MongoDB;
using System;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.Persistance.Base
{
    public sealed class DatabaseTransaction : IDatabaseTransaction
    {
        #region Fields

        private readonly IMongoContext context;

        #endregion

        #region Constructor

        public DatabaseTransaction(IMongoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region IDatabaseTransaction Methods

        public async Task<bool> CommitTransaction()
            => await context.SaveChangesAsync() > 0;

        public void Dispose()
            => context?.Dispose();

        #endregion
    }
}
