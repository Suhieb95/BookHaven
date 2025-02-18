namespace LibrarySystem.Domain.Specification.Users;
public class GetUserByIdSpecification : Specification
{
    public GetUserByIdSpecification(Guid id) => Parameters = new { Id = id };
    public override string ToSql() => "SPGetUserById";
}