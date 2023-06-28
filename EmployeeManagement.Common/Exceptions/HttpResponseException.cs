using System.Net;

namespace EmployeeManagement.Common.Exceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(string message, Exception? innerException = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
