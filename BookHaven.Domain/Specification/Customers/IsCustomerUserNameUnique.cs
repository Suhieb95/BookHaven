namespace BookHaven.Domain.Specification.Customers;
public class IsCustomerUserNameUnique : Specification<bool>
{
    public IsCustomerUserNameUnique(string userName, Guid? id = null) => Parameters = new { UserName = userName.Trim().ToLower(), Id = id };
    public override string ToSql() => "SELECT 1 WHERE EXISTS (SELECT 1 FROM Customers WHERE TRIM(LOWER(UserName)) = @UserName AND Id <> @Id )";
}