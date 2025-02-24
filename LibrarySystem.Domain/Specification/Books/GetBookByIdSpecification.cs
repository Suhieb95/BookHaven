namespace LibrarySystem.Domain.Specification.Books;
public class GetBookByIdSpecification : Specification
{
    public GetBookByIdSpecification(int id) => Parameters = new { Id = id };
    public override string ToSql() => "SPGetBookById";
}
