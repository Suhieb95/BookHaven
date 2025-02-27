namespace BookHaven.Domain.Specification.Authors;
public class IsAuthorNameUnique : Specification<bool>
{
    public IsAuthorNameUnique(string name, int id) => Parameters = new { Name = name.Trim().ToLower(), Id = id };
    public override string ToSql() => "SELECT 1 WHERE EXISTS (SELECT 1 FROM Authors WHERE TRIM(LOWER(Name)) = @Name AND Id <> @Id)";
}