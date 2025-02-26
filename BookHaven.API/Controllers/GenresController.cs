using BookHaven.API.Common.Constants;
using BookHaven.Application.Genres;
using BookHaven.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace BookHaven.API.Controllers;
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
    [HttpDelete(Genres.Delete)]
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
    [HttpGet(Genres.GetById)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        Result<Genre>? result = await _genreApplicationService.GetById(id, cancellationToken);
        return result.Map(
            onSuccess: Ok,
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