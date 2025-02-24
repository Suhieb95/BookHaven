using LibrarySystem.Application.Genres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.API.Controllers;

[AllowAnonymous]
public class GenreController(IGenreApplicationService _genreApplicationService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add(Genre genre)
    {
        Result<int>? result = await _genreApplicationService.Add(genre);
        return result.Map(
            onSuccess: _ => Ok(new Genre() { Id = result.Data, Name = genre.Name }),
            onFailure: Problem
        );
    }
    // [HttpDelete]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     Result<int>? result = await _genreApplicationService.Delete(id);
    //     return result.Map(
    //         onSuccess: NotContent,
    //         onFailure: Problem
    //     );
    // }
}
