namespace LibrarySystem.Domain.Specification.Books;
public class GetBookByIdSpecification : Specification
{
    public GetBookByIdSpecification(int id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql() => "SPGetBookById";
}
