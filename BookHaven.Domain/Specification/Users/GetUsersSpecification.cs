namespace BookHaven.Domain.Specification.Users;
public class GetUsersSpecification : Specification
{
    public override string ToSql() => "SPGetUsers";
}
