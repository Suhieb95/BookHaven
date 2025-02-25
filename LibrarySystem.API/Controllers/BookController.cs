using LibrarySystem.API.Common.Constants;
using LibrarySystem.Application.Books;
using LibrarySystem.Domain.DTOs.Books;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
public class BookController(IBookApplicationService _bookApplicationService) : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] BookPaginationParam param, CancellationToken cancellationToken)
    {
        var result = await _bookApplicationService.GetBooks(param, cancellationToken);
        return result.Map(
                      onSuccess: Ok,
                      onFailure: Problem);
    }
    [AllowAnonymous]
    [HttpGet(Books.GetById)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _bookApplicationService.GetBookById(id, cancellationToken);
        return result.Map(
                      onSuccess: Ok,
                      onFailure: Problem);
    }
    [HttpPost]
    [Authorize(Policy = CustomPolicies.ExcludeNewUserPolicy)]
    [HasPermission(Permission.Create, EntityName.Books)]
    public async Task<IActionResult> Create([FromForm] CreateBookRequest request, CancellationToken cancellationToken)
    {
        Result<int>? result = await _bookApplicationService.CreateBook(request, cancellationToken);
        return result.Map(
                      onSuccess: _ => Ok(_),
                      onFailure: Problem);
    }
}