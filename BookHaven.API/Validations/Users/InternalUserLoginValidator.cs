using FluentValidation;
using BookHaven.Domain.DTOs.Users;

namespace BookHaven.API.Validations.Users;
public class InternalUserLoginValidator : AbstractValidator<InternalUserLoginRequest>
{
    public InternalUserLoginValidator()
    {
        Include(new LoginBaseRequestValidator());
    }
}