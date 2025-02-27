namespace BookHaven.Domain.Specification.Users;
public class IsInternalUserUserNameUnique : Specification<bool>
{
    public IsInternalUserUserNameUnique(string userName, Guid? id = null) => Parameters = new { UserName = userName.Trim().ToLower(), Id = id };
    public override string ToSql() => "SELECT 1 WHERE EXISTS (SELECT 1 FROM Users WHERE TRIM(LOWER(UserName)) = @UserName AND Id <> @Id )";
}