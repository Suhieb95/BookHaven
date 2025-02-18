using LibrarySystem.Application.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.API.Controllers;
public class BookController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        await Task.CompletedTask;
        return Ok();
    }
}
