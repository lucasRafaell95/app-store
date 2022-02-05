using Appstore.Common.Application.Models.Default;
using AppStore.App.Core.Caching.Contracts;
using AppStore.App.Domain.Repositories;
using AppStore.Common.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.App.Core.Queries.Handlers
{
    public sealed class AppQueryHandler : IRequestHandler<GetAppsQuery, Response>
    {
        #region Fields

        private readonly IApplicationCacheService cacheService;
        private readonly IApplicationRepository applicationRepository;

        #endregion

        #region Constructor

        public AppQueryHandler(IApplicationCacheService cacheService,
                               IApplicationRepository applicationRepository)
        {
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            this.applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
        }

        #endregion

        #region Public Methods

        public async Task<Response> Handle(GetAppsQuery request, CancellationToken cancellationToken)
        {
            var result = await cacheService.GetApplicationsAsync(GetFromDatabase);

            return await Task.FromResult(new Response
            {
                Data = result
            });
        }

        #endregion

        #region Private Methods

        private Task<List<Application>> GetFromDatabase()
            => applicationRepository.GetAll();

        #endregion
    }
}
