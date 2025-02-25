namespace BookHaven.Domain.Specification.Users;
public class GetUserById : Specification
{
    public GetUserById(Guid id) => Parameters = new { Id = id };
    public override string ToSql() => "SPGetUserById";
}