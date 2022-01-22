using AppStore.App.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AppStore.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AppController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public AppController(IMediator mediator, ILogger<AppController> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("apps")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppAsync()
        {
            var response = await mediator.Send(new GetAppsQuery());

            return Ok(response);
        }
    }
}
