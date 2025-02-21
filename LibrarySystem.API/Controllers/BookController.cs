using LibrarySystem.Application.Books;
using LibrarySystem.Domain.DTOs.Books;
using Microsoft.AspNetCore.Authorization;
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
}