namespace BookHaven.Domain.Specification.Users;
public class GetUsersSpecification : Specification
{
    public GetUsersSpecification()
        => CommandType = StoredProcedure;
    public override string ToSql() => "SPGetUsers";
}
