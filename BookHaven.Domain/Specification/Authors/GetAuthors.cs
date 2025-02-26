using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Authors;
public class GetAuthors : Specification<Author>
{
    public override string ToSql() => "SELECT * FROM Authors";
}