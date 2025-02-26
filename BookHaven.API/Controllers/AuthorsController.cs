using BookHaven.API.Common.Constants;
using BookHaven.Application.Authors;
using Microsoft.AspNetCore.Mvc;

namespace BookHaven.API.Controllers;
[Authorized(Policy = CustomPolicies.ExcludeNewUserPolicy)]
public class AuthorsController(IAuthorApplicationService _authorApplicationService) : BaseController
{
    [HttpGet(Authors.GetById)]
    [HasPermission(Permission.Read, EntityName.Authors)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _authorApplicationService.GetById(id, cancellationToken);
        return result.Map(
                   onSuccess: Ok,
                   onFailure: Problem
               );
    }
    [HttpGet]
    [HasPermission(Permission.Read, EntityName.Authors)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<List<Author>>? result = await _authorApplicationService.GetAll(cancellationToken);
        return result.Map(
            onSuccess: Ok,
            onFailure: Problem
        );
    }
    [HttpPost]
    [HasPermission(Permission.Create, EntityName.Authors)]
    public async Task<IActionResult> Create(Author author, CancellationToken cancellationToken)
    {
        Result<int>? result = await _authorApplicationService.Create(author, cancellationToken);
        return result.Map(
            onSuccess: _ => CreatedAtAction(nameof(Create), _),
            onFailure: Problem
        );
    }
    [HttpDelete(Authors.Delete)]
    [HasPermission(Permission.Delete, EntityName.Authors)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _authorApplicationService.Delete(id, cancellationToken);
        return result.Map(
            onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
    [HttpPut]
    [HasPermission(Permission.Update, EntityName.Authors)]
    public async Task<IActionResult> Update(Author author, CancellationToken cancellationToken)
    {
        Result<bool>? result = await _authorApplicationService.Update(author, cancellationToken);
        return result.Map(
          onSuccess: _ => NoContent(),
            onFailure: Problem
        );
    }
}