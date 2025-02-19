using LibrarySystem.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.API.Controllers;
public class ErrorController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public IActionResult Error()
    {
        Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error!;
        var (statusCode, message, title) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage, serviceException.Title),
            _ => (StatusCodes.Status500InternalServerError, "An Error has Occurred!", "Internal Server Error")
        };
        return Problem(statusCode: statusCode, title: title, detail: message);
    }
}
