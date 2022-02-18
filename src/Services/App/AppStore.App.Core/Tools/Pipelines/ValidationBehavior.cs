using Appstore.Common.Application.Models.Default;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.App.Core.Tools.Pipelines
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                  where TResponse : Response
    {
        #region Fields

        private readonly IEnumerable<IValidator<TRequest>> validators;

        #endregion

        #region Constructor

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        #endregion

        #region IPipelineBehavior Methods

        public async Task<TResponse> Handle(TRequest request, CancellationToken token, RequestHandlerDelegate<TResponse> next)
        {
            var failures = validators
                               .Select(x => x.Validate(request))
                               .SelectMany(x => x.Errors)
                               .Where(x => x != null)
                               .ToList();

            return failures.Any() ? await Errors(failures) : await next();
        }

        #endregion

        #region Private Methods

        private async Task<TResponse> Errors(List<ValidationFailure> failures)
        {
            var response = new Response
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Unable to process transaction. See the field - ErrorMessages - for more details."
            };

            failures.ForEach(e =>
            {
                response.AddErrorMessage(e.ErrorMessage);
            });

            return await Task.FromResult(response) as TResponse;
        }

        #endregion
    }
}
