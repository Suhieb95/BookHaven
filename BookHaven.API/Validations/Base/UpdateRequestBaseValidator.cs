using BookHaven.API.Helpers;
using BookHaven.Application.Helpers;
using BookHaven.Domain.DTOs.BaseModels;
using FluentValidation;

namespace BookHaven.API.Validations.Base;
public class UpdateRequestBaseValidator : AbstractValidator<UpdateRequestBase>
{
    public UpdateRequestBaseValidator()
    {
        RuleFor(s => s.EmailAddress).EmailAddress()
                                                     .WithMessage("A valid email is required");
        RuleFor(x => x.Password).NotEmpty()
                                                        .WithMessage("Password Is required.")
                                                        .Password();
        RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name Is required.")
                                                .MinimumLength(4)
                                                .WithMessage("User Name has to be at Least 4 characters Long.")
                                                .Matches(@"^\S+$").WithMessage("User Name cannot contain spaces.");
        RuleFor(x => x.Image).Must(Extensions.IsValidImageFormat)
                                            .WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg.");
    }
}