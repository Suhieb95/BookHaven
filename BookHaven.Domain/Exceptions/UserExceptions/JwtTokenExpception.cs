using System.Net;
namespace BookHaven.Domain.Exceptions.UserExceptions;

public class JwtTokenExpception : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Cannot Generate Token, Invalid User.";
    public string Title => "Error";
}
