namespace BookHaven.Domain.Specification.Books;
public class IsBookISBNUniqueSpecification : Specification<bool>
{
    public IsBookISBNUniqueSpecification(string isbn, int? id = null) => Parameters = new { ISBN = isbn.Trim(), Id = id };
    public override string ToSql() => "SELECT 1 WHERE EXISTS (SELECT 1 FROM Books WHERE TRIM(ISBN) = @ISBN AND Id <> @Id )";
}