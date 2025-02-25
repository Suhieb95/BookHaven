using System.Net;

namespace BookHaven.Domain.Exceptions;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
    public string Title { get; }
}
