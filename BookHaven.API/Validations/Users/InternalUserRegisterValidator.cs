using FluentValidation;
using BookHaven.Domain.DTOs.Users;
namespace BookHaven.API.Validations.Users;
public class InternalUserRegisterValidator : AbstractValidator<InternalUserRegisterRequest>
{
    public InternalUserRegisterValidator()
    {
        Include(new RegisterBaseRequestValidator());
    }
}