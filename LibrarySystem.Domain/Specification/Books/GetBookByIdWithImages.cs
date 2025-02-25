namespace LibrarySystem.Domain.Specification.Books;
public class GetBookByIdWithImages : Specification
{
    public GetBookByIdWithImages(int id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql() => "SPGetBookByIdWithImages";
}
