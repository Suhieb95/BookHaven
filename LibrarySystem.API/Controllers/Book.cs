using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.API.Controllers;
public class Book : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        return Ok();
    }
}
