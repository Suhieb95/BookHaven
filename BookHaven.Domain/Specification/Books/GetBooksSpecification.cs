using BookHaven.Domain.DTOs;

namespace BookHaven.Domain.Specification.Books;
public class GetBooksSpecification : Specification
{
    public GetBooksSpecification(PaginationParam paginationParam) => Parameters = paginationParam;
    public override string ToSql() => "SPGetBooks";
}
