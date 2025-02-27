using FluentValidation;
using BookHaven.Domain.DTOs.Books;
using Extensions = BookHaven.Application.Helpers.Extensions;
namespace BookHaven.API.Validations.Book;
public class UpdateBookImageRequestValidator : AbstractValidator<UpdateBookImageRequest>
{
    public UpdateBookImageRequestValidator()
    {
        RuleFor(x => x.Images)
      .NotNull().WithMessage("Images cannot be null.")
      .ForEach(x => x
          .NotNull().WithMessage("Each image in the list cannot be null.")
          .Must(Extensions.IsValidImageFormat)
          .WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg."));
    }
}