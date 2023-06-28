using System.Net;

namespace EmployeeManagement.Common.Exceptions
{
    public class EntityNotFoundException : HttpResponseException
    {
        public EntityNotFoundException(string message, Exception? innerException = null)
            : base(message, innerException, HttpStatusCode.NotFound)
        {
        }
    }
}
