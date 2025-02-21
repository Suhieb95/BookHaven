using System.Net;
namespace LibrarySystem.Domain.Exceptions.FileUploadExceptions;

public class FileUploadException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Upload Failed.";
    public string Title => "Error";
}
