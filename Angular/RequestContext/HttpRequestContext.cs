using EmployeeManagement.Common.RequestContext;

namespace EmployeeManager.Api.RequestContext
{
    public class HttpRequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpRequestContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public long? CurrentUserId => IsAuthenticated
            ? long.Parse(HttpContext.User.Claims.First(x => x.Type == "Id").Value)
            : null;

        public string? CurrentUsername => IsAuthenticated
            ? HttpContext.User.Claims.First(x => x.Type == "Name").Value
            : "Nem bejelentkezett felhasználó";

        public CancellationToken RequestAborted => HttpContext.RequestAborted;

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;
    }
}
