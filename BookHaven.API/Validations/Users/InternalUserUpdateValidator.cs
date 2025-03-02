using FluentValidation;
using BookHaven.Domain.DTOs.Users;
namespace BookHaven.API.Validations.Users;
public class InternalUserUpdateValidator : AbstractValidator<InternalUserUpdateRequest>
{
    public InternalUserUpdateValidator()
    {
        Include(new UpdateRequestBaseValidator());
    }
}