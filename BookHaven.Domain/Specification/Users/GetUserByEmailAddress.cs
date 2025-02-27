using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Users;
public class GetUserByEmailAddress : Specification<User>
{
    public GetUserByEmailAddress(string emailAddress) => Parameters = new { emailAddress };
    public override string ToSql() => "SELECT * FROM Users WHERE EmailAddress = TRIM(LOWER(@EmailAddress))";
}