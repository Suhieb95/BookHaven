namespace BookHaven.Domain.Specification.Customers;
public class GetCustomerByEmailAddress : Specification
{
    public GetCustomerByEmailAddress(string emailAddress)
        => (Parameters, CommandType) = (new { EmailAddress = emailAddress.Trim().ToLower() }, StoredProcedure);

    public override string ToSql() => "SPGetCustomerByEmail";
}
