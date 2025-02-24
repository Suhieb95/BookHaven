namespace LibrarySystem.Domain.Specification.Users;
public class GetUserByEmailAddress : Specification
{
    public GetUserByEmailAddress(string emailAddress) => Parameters = new { emailAddress };
    public override string ToSql() => "SELECT * FROM Users WHERE TRIM(LOWER(EmailAddress)) = TRIM(LOWER(@EmailAddress))";
}