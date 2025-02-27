using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Customers;
using BookHaven.Domain.Specification.Users;

namespace BookHaven.Application.Authentication.Common;
internal static class CheckUserNameExistence
{
    internal static async Task<(bool, Result<T>?)> ValidateUserName<T>(IUnitOfWork unitOfWork, string userName, Guid? id = null, UserType userType = UserType.Customer, CancellationToken? cancellationToken = default)
    {
        switch (userType)
        {
            case UserType.Internal:
                {
                    bool isDuplicateUserName = await unitOfWork.Users.GetBy(new IsInternalUserUserNameUnique(userName, id), cancellationToken);
                    if (isDuplicateUserName)
                        return (false, Result<T>.Failure(new Error("User Name already exists.", BadRequest, "Invalid User Name")));
                }
                break;
            case UserType.Customer:
                {
                    bool isDuplicateUserName = await unitOfWork.Customers.GetBy(new IsCustomerUserNameUnique(userName, id), cancellationToken);
                    if (isDuplicateUserName)
                        return (false, Result<T>.Failure(new Error("User Name already exists.", BadRequest, "Invalid User Name")));
                }
                break;

            default:
                break;
        }

        return (true, null);
    }
}
