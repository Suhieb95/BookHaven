using FluentValidation;
using BookHaven.Domain.DTOs.BaseModels;

namespace BookHaven.API.Validations.Base;
public class RegisterBaseRequestValidator : AbstractValidator<RegisterBaseRequest>
{
    public RegisterBaseRequestValidator()
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