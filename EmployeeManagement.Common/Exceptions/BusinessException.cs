using System.Net;

namespace EmployeeManagement.Common.Exceptions
{
    public class BusinessException : HttpResponseException
    {
        public BusinessException(string message, Exception? innerException = null)
            : base(message, innerException, HttpStatusCode.BadRequest)
        {
        }
    }
}
