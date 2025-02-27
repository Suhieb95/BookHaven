using BookHaven.Domain.Entities;
namespace BookHaven.Domain.Specification.Genres;
public class GetGenreById : Specification<Genre>
{
    public GetGenreById(int id)
             => Parameters = new { Id = id };
    public override string ToSql()
             => "Select * from Genres where Id = @Id;";
}