using LibrarySystem.Application.Genres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.API.Controllers;

[AllowAnonymous]
public class GenreController(IGenreApplicationService _genreApplicationService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(Genre genre, CancellationToken cancellationToken)
    {
        Result<int>? result = await _genreApplicationService.Add(genre, cancellationToken);
        return result.Map(
            onSuccess: data => Ok(data),
            onFailure: Problem
        );
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _genreApplicationService.Delete(id, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
}
