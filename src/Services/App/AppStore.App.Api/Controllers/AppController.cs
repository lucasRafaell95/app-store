using Appstore.Common.Application.Models.Default;
using AppStore.App.Core.Commands;
using AppStore.App.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AppStore.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class AppController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMediator mediator;

        public AppController(IMediator mediator, ILogger<AppController> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Returns all applications already registered
        /// </summary>
        /// <returns>Returns all applications already registered</returns>
        [HttpGet]
        [Route("Apps")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAppAsync()
        {
            try
            {
                var response = await mediator.Send(new GetAppsQuery());

                return response.Data != null
                    ? Ok(response)
                    : NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetAppAsync");

                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Message = "An error occurred",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }

        /// <summary>
        /// Create a new application
        /// </summary>
        /// <param name="command">Command representing the application that must be inserted into the database</param>
        /// <returns>Returns the Response object with the result of the request</returns>
        /// <response code="200">Returned when the creation of a new application was successful</response>
        /// <response code="204">Returned when some other application of the same name already exists</response>
        /// <response code="408">Returned when an error occurs in the processing of the request</response>
        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<IActionResult> CreateAppAsync([FromBody] CreateApplicationCommand command)
        {
            try
            {
                var result = await mediator.Send(command);

                return result.Success
                    ? Ok(result)
                    : BadRequest(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CreateAppAsync", new
                {
                    Command = command
                });

                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Message = "An error occurred",
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
        }
    }
}
