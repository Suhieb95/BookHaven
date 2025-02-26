namespace BookHaven.Domain.Specification.Genres;
public class GetUsedGenre : Specification<bool>
{
     public GetUsedGenre(int id)
          => Parameters = new { Id = id };
     public override string ToSql()
          => "Select 1 where exists (select 1 from BookGenres where GenreId = @Id);";
}