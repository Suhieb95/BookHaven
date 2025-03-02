using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Customers;
using BookHaven.Domain.Specification.Users;

namespace BookHaven.Application.Authentication.Common;
internal static class CheckUserNameExistence
{
    internal static async Task<(bool, Result<T>?)> ValidateUserName<T>(
        IUnitOfWork unitOfWork,
        string userName,
        Guid? id = null,
        UserType userType = UserType.Customer,
        CancellationToken? cancellationToken = default)
    {
        cancellationToken ??= CancellationToken.None;

        bool isDuplicateUserName = await IsUserNameUnique(unitOfWork, userName, id, userType, cancellationToken.Value);

        if (isDuplicateUserName)
        {
            var error = new Error("User Name already exists.", BadRequest, "Invalid User Name");
            return (false, Result<T>.Failure(error));
        }

        return (true, null);
    }
    private static async Task<bool> IsUserNameUnique(
       IUnitOfWork unitOfWork,
       string userName,
       Guid? id,
       UserType userType,
       CancellationToken cancellationToken)
       => userType switch
       {
           UserType.Internal => await unitOfWork.Users.GetBy(new IsInternalUserUserNameUnique(userName, id), cancellationToken),
           UserType.Customer => await unitOfWork.Customers.GetBy(new IsCustomerUserNameUnique(userName, id), cancellationToken),
           _ => throw new ArgumentOutOfRangeException($"Invalid UserType: {userType}")
       };
}