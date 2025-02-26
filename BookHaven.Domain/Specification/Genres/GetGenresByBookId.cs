namespace BookHaven.Domain.Specification.Genres;
public class GetGenresByBookId : Specification<string>
{
    public GetGenresByBookId(int id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql()
             => "SPGetBookGenres";
}
