using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Customers;
public class GetCustomerByEmailAddress : Specification<Customer>
{
    public GetCustomerByEmailAddress(string emailAddress)
        => (Parameters, CommandType) = (new { EmailAddress = emailAddress.Trim().ToLower() }, StoredProcedure);

    public override string ToSql() => "SPGetCustomerByEmail";
}
