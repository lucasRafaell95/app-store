using Appstore.Common.Application.Models.Default;
using Appstore.Common.Application.Models.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.App.Core.Queries.Handlers
{
    public sealed class AppQueryHandler : IRequestHandler<GetAppsQuery, Response>
    {
        public Task<Response> Handle(GetAppsQuery request, CancellationToken cancellationToken)
        {
            var result = new Response
            {
                Data = new List<AppDTO>
                {
                    new AppDTO
                    {
                        Id = 10,
                        Name = "whatsapp",
                        Price = 10,
                        Description = "aplicativo de mensagens"
                    }
                }
            };

            return Task.FromResult(result);
        }
    }
}
