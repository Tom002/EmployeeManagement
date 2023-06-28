using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Net;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerDefaultResponse]
    [SwaggerResponse(HttpStatusCode.BadRequest, typeof(ValidationProblemDetails))]
    [SwaggerResponse(HttpStatusCode.NotFound, typeof(ProblemDetails))]
    [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ProblemDetails))]
    [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(ProblemDetails))]
    public class BaseController : ControllerBase
    {

    }
}
