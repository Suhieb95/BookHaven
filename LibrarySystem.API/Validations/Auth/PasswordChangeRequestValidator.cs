using FluentValidation;
using LibrarySystem.API.Helpers;
using LibrarySystem.Domain.DTOs.Auth;
namespace LibrarySystem.API.Validations.Auth;
public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeRequestValidator()
    {
        RuleFor(s => s.UserId).NotEmpty()
                                                    .WithMessage("Invalid User Id.");
        RuleFor(x => x.Password).NotEmpty()
                                                    .WithMessage("Password Is required.")
                                                    .Password();
    }
}