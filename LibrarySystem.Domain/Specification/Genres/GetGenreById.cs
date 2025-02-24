namespace LibrarySystem.Domain.Specification.Genres;
public class GetGenreById : Specification
{
    public GetGenreById(int id)
             => Parameters = new { Id = id };
    public override string ToSql()
             => "Select * from Genres where Id = @Id;";
}
