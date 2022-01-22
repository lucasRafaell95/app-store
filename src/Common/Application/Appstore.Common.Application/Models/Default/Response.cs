using System.Collections.Generic;
using System.Net;

namespace Appstore.Common.Application.Models.Default
{
    public sealed class Response
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public void AddErrorMessage(string message)
            => this.ErrorMessages.Add(message);
    }
}
