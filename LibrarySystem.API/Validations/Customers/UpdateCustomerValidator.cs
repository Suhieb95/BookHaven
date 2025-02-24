using FluentValidation;
using LibrarySystem.API.Helpers;
using LibrarySystem.Application.Helpers;
using LibrarySystem.Domain.DTOs.Customers;
namespace LibrarySystem.API.Validations.Customers;
public class UpdateUserValidator : AbstractValidator<CustomerUpdateRequest>
{
    public UpdateUserValidator()
    {

        RuleFor(s => s.EmailAddress).EmailAddress()
                                                        .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
        RuleFor(x => x.UserName).NotEmpty()
                                                        .WithMessage("User Name Is required.")
                                                        .MinimumLength(4)
                                                        .WithMessage("User Name has to be at Least 4 characters Long.")
                                                        .Matches(@"^\S+$").WithMessage("User Name cannot contain spaces.");
        RuleFor(x => x.Image)
                    .Must(Extensions.IsValidImageFormat)
                    .WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg.");
    }
}