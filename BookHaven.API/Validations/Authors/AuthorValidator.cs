using FluentValidation;

using Author = BookHaven.Domain.Entities.Author;

namespace BookHaven.API.Validations.Authors;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now.AddYears(-4)).WithMessage("Author must be at least 4 years old.");
    }
}
