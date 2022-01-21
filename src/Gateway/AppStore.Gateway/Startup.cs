using AppStore.Gateway.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace AppStore.Gateway
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
            => services.AddOcelot(configuration).AddDelegatingHandler<RequestHandler>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app.UseOcelot().Wait();
    }
}
