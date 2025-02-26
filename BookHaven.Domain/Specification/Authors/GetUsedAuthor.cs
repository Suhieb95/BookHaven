namespace BookHaven.Domain.Specification.Authors;
public class GetUsedAuthor : Specification<bool>
{
     public GetUsedAuthor(int id)
          => Parameters = new { Id = id };
     public override string ToSql()
          => "Select 1 where exists (select 1 from BookAuthors where AuthorId = @Id);";
}