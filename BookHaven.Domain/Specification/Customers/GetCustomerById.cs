using BookHaven.Domain.Entities;

namespace BookHaven.Domain.Specification.Customers;
public class GetCustomerById : Specification<Customer>
{
    public GetCustomerById(Guid id) => Parameters = new { Id = id };
    public override string ToSql() => "SELECT * FROM Customers WHERE Id = @Id";
}
