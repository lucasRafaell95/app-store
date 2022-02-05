using Appstore.Common.Application.Models.Default;
using MediatR;

namespace AppStore.App.Core.Queries
{
    public sealed record GetAppsQuery : IRequest<Response> { }
}
