using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AppStore.App.Core.Extensions
{
    public static class AppCoreExtension
    {
        public static IServiceCollection AddAppCore(this IServiceCollection services)
            => services.AddMediator();

        private static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(typeof(AppCoreExtension).Assembly);
    }
}
