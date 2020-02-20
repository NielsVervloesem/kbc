using System;
using System.Net;
using System.Net.Http;

namespace KBCFoodAndGo.Shared.Exceptions
{
    [Serializable]
    public class HttpRequestExceptionException : HttpRequestException
    {
        public HttpStatusCode HttpCode { get; }

        public HttpRequestExceptionException(HttpStatusCode code) : this(code, null, null)
        {
        }

        public HttpRequestExceptionException(HttpStatusCode code, string message) : this(code, message, null)
        {
        }

        public HttpRequestExceptionException(HttpStatusCode code, string message, Exception inner) : base(message, inner)
        {
            HttpCode = code;
        }
    }
}