using Appstore.Common.Application.Models.Default;
using MediatR;

namespace AppStore.App.Core.Commands
{
    public sealed record CreateApplicationCommand : IRequest<Response>
    {
        public string Name { get; init; }
        public double Price { get; init; }
        public string Description { get; init; }
    }
}
