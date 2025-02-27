using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Genres;
public class GetGenreByName : Specification<Genre>
{
     public GetGenreByName(string name, int? id = null)
     {
          Parameters = new { name, id };
          CommandType = StoredProcedure;
     }
     public override string ToSql()
          => "SPGetGenreByName";
}