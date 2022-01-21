using System.Collections.Generic;
using System.Net;

namespace Appstore.Common.Application.Models.Default
{
    public sealed class Response
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public object Collections { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public IList<string> ErrorMessages { get; set; } = new List<string>();

        public void AddErrorMessage(string message)
            => this.ErrorMessages.Add(message);
    }
}
