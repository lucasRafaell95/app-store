using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppStore.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AppController : ControllerBase
    {
        private readonly ILogger<AppController> logger;

        public AppController(ILogger<AppController> logger)
        {
            this.logger = logger;
        }
    }
}
