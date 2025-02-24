namespace LibrarySystem.Domain.Specification.Genres;
public class GetUsedGenre : Specification
{
    public GetUsedGenre(int id)
    => Parameters = new { Id = id };
    
    public override string ToSql()
    => "Select * from Genres where exists (select 1 from Books b where b.GenreId = @Id);";
    
}
