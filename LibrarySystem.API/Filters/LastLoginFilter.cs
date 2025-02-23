using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.BaseModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace LibrarySystem.API.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class LastLoginFilter(IUnitOfWork _unitOfWork) : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        // Inspect the result before continuing execution
        IActionResult? result = context.Result;

        if (result is OkObjectResult okObj)
        {
            AuthenticatedUserBase? res = (AuthenticatedUserBase?)okObj.Value;
            if (res is not null)
                await _unitOfWork.Users.LastLogin(res.EmailAddress, res.PersonType);
        }

        // Continue with the result execution after your logic
        await next();
    }
}

