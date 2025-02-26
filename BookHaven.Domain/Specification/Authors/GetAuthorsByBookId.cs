namespace BookHaven.Domain.Specification.Authors;
public class GetAuthorsByBookId : Specification<string>
{
    public GetAuthorsByBookId(int id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql()
             => "SPGetBookAuthors";
}