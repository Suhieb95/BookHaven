using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Authors;
public class GetAuthorById : Specification<Author>
{
    public GetAuthorById(int id) => Parameters = new { Id = id };
    public override string ToSql() => "SELECT * FROM Authors WHERE Id = @Id";
}