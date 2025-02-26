using BookHaven.Domain.DTOs;

namespace BookHaven.Domain.Specification.Books;
public class GetBooksSpecification : Specification
{
    public GetBooksSpecification(PaginationParam paginationParam)
        => (Parameters, CommandType) = (paginationParam, StoredProcedure);
    public override string ToSql() => "SPGetBooks";
}