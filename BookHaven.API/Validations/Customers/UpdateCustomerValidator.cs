using FluentValidation;
using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.API.Validations.Customers;
public class UpdateUserValidator : AbstractValidator<CustomerUpdateRequest>
{
    public UpdateUserValidator()
    {
        Include(new UpdateRequestBaseValidator());
    }
}