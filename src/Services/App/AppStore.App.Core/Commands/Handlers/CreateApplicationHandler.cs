using Appstore.Common.Application.Models.Default;
using AppStore.App.Core.Models.DTO;
using AppStore.App.Domain.Repositories;
using AppStore.Common.Domain.Entities;
using AppStore.Common.Domain.Persistence.Base;
using Mapster;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.App.Core.Commands.Handlers
{
    public sealed class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, Response>
    {
        #region Fields

        private readonly IDatabaseTransaction transaction;
        private readonly IApplicationRepository applicationRepository;

        #endregion

        #region Constructor

        public CreateApplicationHandler(IApplicationRepository applicationRepository, IDatabaseTransaction transaction)
        {
            this.applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
            this.transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        #endregion

        #region Handles

        public async Task<Response> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var app = await applicationRepository.GetApplicationByNameAsync(request.Name);

            if (app is null)
            {
                applicationRepository.Create(request.Adapt<Application>());

                await transaction.CommitTransaction();

                return await Task.FromResult(new Response { Data = request.Adapt<ApplicationDTO>() });
            }

            return await Task.FromResult(new Response
            {
                Message = $"An application named {request.Name} already exists",
                Success = false,
                StatusCode = HttpStatusCode.BadRequest
            });
        }

        #endregion
    }
}
