using LibrarySystem.API.Common.Constants;
using LibrarySystem.Application.Genres;
using LibrarySystem.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.API.Controllers;
// Permission to be added
[Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
public class GenresController(IGenreApplicationService _genreApplicationService) : BaseController
{
    [HasPermission(Permission.Create, EntityName.Genres)]
    [HttpPost]
    public async Task<IActionResult> Add(Genre genre, CancellationToken cancellationToken)
    {
        Result<int>? result = await _genreApplicationService.Add(genre, cancellationToken);
        return result.Map(
            onSuccess: data => Ok(data),
            onFailure: Problem
        );
    }
    [HasPermission(Permission.Delete, EntityName.Genres)]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _genreApplicationService.Delete(id, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
    [HasPermission(Permission.Update, EntityName.Genres)]
    [HttpPut]
    public async Task<IActionResult> Update(Genre genre, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _genreApplicationService.Update(genre, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
    [HasPermission(Permission.Read, EntityName.Genres)]
    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] PaginationParam paginationParam, CancellationToken cancellationToken)
    {
        Result<PaginatedResponse<Genre>>? result = await _genreApplicationService.GetPaginatedGenres(paginationParam, cancellationToken);
        return result.Map(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
}