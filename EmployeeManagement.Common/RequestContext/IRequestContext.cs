namespace EmployeeManagement.Common.RequestContext
{
    public interface IRequestContext
    {
        bool IsAuthenticated { get; }
        long? CurrentUserId { get; }
        string? CurrentUsername { get; }
        CancellationToken RequestAborted { get; }
    }
}
