using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Genres;
public class GetGenreByName : Specification<Genre>
{
     public GetGenreByName(string name)
     {
          Parameters = new { name };
          CommandType = StoredProcedure;
     }
     public override string ToSql()
          => "SPGetGenreByName";
}