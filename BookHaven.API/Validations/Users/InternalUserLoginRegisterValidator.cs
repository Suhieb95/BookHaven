using FluentValidation;
using BookHaven.Domain.DTOs.Users;
namespace BookHaven.API.Validations.Users;
public class InternalUserLoginRegisterValidator : AbstractValidator<InternalUserRegisterRequest>
{
    public InternalUserLoginRegisterValidator()
    {

        RuleFor(s => s.EmailAddress).EmailAddress()
                                                         .WithMessage("A valid email is required");
        RuleFor(x => x.UserName).NotEmpty()
                                                        .WithMessage("User Name Is required.")
                                                        .MinimumLength(4)
                                                        .WithMessage("User Name has to be at Least 4 characters Long.")
                                                        .Matches(@"^\S+$").WithMessage("User Name cannot contain spaces.");
    }
}