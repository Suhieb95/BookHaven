namespace BookHaven.Domain.Specification.Books;
public class IsBookTitleUniqueSpecification : Specification<bool>
{
    public IsBookTitleUniqueSpecification(string title, int? id = null) => Parameters = new { Title = title.Trim().ToLower(), Id = id };
    public override string ToSql() => "SELECT 1 WHERE EXISTS (SELECT 1 FROM Books WHERE TRIM(LOWER(Title)) = @Title AND Id <> @Id )";
}
