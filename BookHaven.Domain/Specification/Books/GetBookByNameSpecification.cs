using BookHaven.Domain.DTOs.Books;

namespace BookHaven.Domain.Specification.Books;
public class GetBookByNameSpecification : Specification<BookResponse>
{
    public GetBookByNameSpecification(string title) => Parameters = new { Title = title.Trim().ToLower() };
    public override string ToSql() => "SELECT * FROM Books WHERE TRIM(LOWER(Title)) = @Title";

}
