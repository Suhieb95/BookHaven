using LibrarySystem.Domain.DTOs.Books;

namespace LibrarySystem.Domain.Specification.Books;
public class GetBookByIdSpecification : Specification<BookResponse>
{
    public GetBookByIdSpecification(int id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql() => "SPGetBookById";
}
