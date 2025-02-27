using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Users;
public class GetUserById : Specification<User>
{
    public GetUserById(Guid id)
    {
        Parameters = new { Id = id };
        CommandType = StoredProcedure;
    }
    public override string ToSql() => "SPGetUserById";
}