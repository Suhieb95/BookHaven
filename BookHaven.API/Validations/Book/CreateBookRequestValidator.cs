using FluentValidation;
using BookHaven.Domain.DTOs.Books;
using Extensions = BookHaven.Application.Helpers.Extensions;
namespace BookHaven.API.Validations.Book;
public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    private readonly int currentYear = DateTime.Now.Year;
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Images).ForEach(x =>
                                                    x.Must(Extensions.IsValidImageFormat).WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg."));
        RuleFor(x => x.PublishedYear)
            .GreaterThanOrEqualTo((short)1600)
            .WithMessage("Published year must be greater than or equal to 1600.")
            .LessThanOrEqualTo((short)currentYear)
            .WithMessage($"Published year must be less than or equal to {currentYear}.");
    }
}