using System.Text.RegularExpressions;
using BookHaven.API.Common.Constants;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookHaven.API.Controllers;

[ApiController]
[Route(ApiEndPoints.BaseController)]
[ApiVersion("1.0")]
[EnableRateLimiting("StandardLimiterPolicy")]
[RequestTimeout("default")]
[Authorized]
public abstract partial class BaseController : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        string statusCodeWithSpaces = MyRegex().Replace(
                                    string.IsNullOrEmpty(error.Title)
                                    ? error.StatusCode.ToString()
                                    : error.Title!, " $1");

        return Problem(statusCode: (int)error.StatusCode, title: statusCodeWithSpaces, detail: error.Message);
    }

    [GeneratedRegex("(\\B[A-Z])")]
    private static partial Regex MyRegex();
}