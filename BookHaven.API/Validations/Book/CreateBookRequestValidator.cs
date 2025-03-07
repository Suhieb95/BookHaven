using FluentValidation;
using BookHaven.Domain.DTOs.Books;
using Extensions = BookHaven.Application.Helpers.Extensions;
namespace BookHaven.API.Validations.Book;
public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
{
    private readonly int _currentYear = DateTime.Now.Year;
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Images).ForEach(x =>
                                                    x.Must(Extensions.IsValidImageFormat).WithMessage("Invalid image format. Allowed formats are: .png, .jpeg, .gif, .jpg."));
        RuleFor(x => x.Price).GreaterThanOrEqualTo(1).WithMessage("Price must be greater than or equal to 0");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");
        RuleFor(x => x.ISBN).Matches(@"^(97[89])?\d{9}(\d|X)$")
                    .WithMessage("Invalid ISBN. Must be a valid ISBN-10 or ISBN-13.");
        RuleFor(x => x.PublishedYear).GreaterThanOrEqualTo((short)1600)
                                                .WithMessage("Published year must be greater than or equal to 1600.")
                                                .LessThanOrEqualTo((short)_currentYear)
                                                .WithMessage($"Published year must be less than or equal to {_currentYear}.");
        RuleFor(x => x.Authors).Must(x => x.Count > 0).WithMessage("Authors cannot be empty.");
        RuleFor(x => x.Genres).Must(x => x.Count > 0).WithMessage("Genres cannot be empty.");
    }
}