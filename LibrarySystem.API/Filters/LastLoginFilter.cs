// using Microsoft.AspNetCore.Mvc.Filters;
// namespace LibrarySystem.API.Filters;
// public class LastLoginFilter : Attribute, IAsyncActionFilter
// {
//     private readonly IUserSerivce _userSerivce;
//     public LastLoginFilter(IUserSerivce userSerivce)
//        => _userSerivce = userSerivce;
//     public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//     {
//         var executedContext = await next();
//         if (executedContext.Exception == null) // Ensure action executed successfully
//         {
//             var email = context.HttpContext.Request.Headers["X-User-Email"].ToString();

//             if (!string.IsNullOrEmpty(email))
//                 await _userSerivce.LastLogin(Guid.Empty, email, true);
//         }
//     }
// }

