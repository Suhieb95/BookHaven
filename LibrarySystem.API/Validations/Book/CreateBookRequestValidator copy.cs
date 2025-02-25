using FluentValidation;
using LibrarySystem.Domain.DTOs.Books;
using Extensions = LibrarySystem.Application.Helpers.Extensions;
namespace LibrarySystem.API.Validations.Book;
public class UpdateBookImageRequestValidator : AbstractValidator<UpdateBookImageRequest>
{
    public UpdateBookImageRequestValidator()
    {
        RuleFor(x => x.Images).ForEach(x =>
                                                    x.Must(Extensions.IsValidImageFormat).WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg."));
    }
}