using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Services;
public interface ICustomerService : IGenericReadWithParamRepository<List<Customer>, Specification>, IGenericWriteRepository<CustomerRegisterRequest, CustomerUpdateRequest, Guid>
{
    Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken);
    Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken);
    Task UpdatePassowordResetToken(PasswordChangeRequest passwordChangeRequest, CancellationToken? cancellationToken);
    Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken);
}