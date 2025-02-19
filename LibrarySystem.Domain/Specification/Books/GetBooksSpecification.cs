using LibrarySystem.Domain.DTOs;

namespace LibrarySystem.Domain.Specification.Books;
public class GetBooksSpecification : Specification
{
    public GetBooksSpecification(PaginationParam paginationParam) => Parameters = paginationParam;
    public override string ToSql() => "SPGetBooks";
}
