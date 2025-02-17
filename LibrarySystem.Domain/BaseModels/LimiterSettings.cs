using System.Net;
namespace LibrarySystem.Domain.BaseModels;
public abstract class LimiterSettings
{
    public string PolicyName { get; init; } = null!;
    public int PermitLimit { get; init; }
    public int Window { get; init; }
    public HttpStatusCode RejectionStatusCode { get; init; }
}
