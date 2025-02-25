namespace BookHaven.Domain.Specification.Genres;
public class GetGenreByName : Specification
{
     public GetGenreByName(string name)
          => Parameters = new { Name = name };
     public override string ToSql()
          => "SPGetGenreByName";


}
