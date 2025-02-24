namespace LibrarySystem.Domain.Specification.Users;
public class GetUserByEmailAddress : Specification
{
    public GetUserByEmailAddress(string emailAddress) => Parameters = new { emailAddress };
    public override string ToSql() => "SELECT * FROM Users WHERE EmailAddress = TRIM(LOWER(@EmailAddress))";
}