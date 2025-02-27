using FluentValidation;
namespace BookHaven.API.Validations.Genres;

public class GenreValidator : AbstractValidator<Genre>
{
    public GenreValidator()
       => RuleFor(x => x.Name).NotEmpty();
}