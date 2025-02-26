using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Genres;
public class GetGenreByName : Specification<Genre>
{
     public GetGenreByName(string name)
          => Parameters = new { Name = name };
     public override string ToSql()
          => "SPGetGenreByName";
}