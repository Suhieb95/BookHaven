namespace LibrarySystem.Domain.Specification.Customers;
public class GetCustomerByEmailAddress : Specification
{
    public GetCustomerByEmailAddress(string emailAddress) => Parameters = new { EmailAddress = emailAddress.Trim().ToLower() };
    public override string ToSql() => "SPGetCustomerByEmail";
}
