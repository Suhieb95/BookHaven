namespace BookHaven.Domain.Specification.Books;

public class GetBookUsed : Specification<bool>
{
    public GetBookUsed(int id)
    {
        Parameters = new { id };
        CommandType = StoredProcedure;
    }
    public override string ToSql() => "SPIsBookUsed";
}