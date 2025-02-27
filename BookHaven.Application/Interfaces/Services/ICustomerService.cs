using BookHaven.Domain.DTOs.Auth;
using BookHaven.Domain.DTOs.Customers;

namespace BookHaven.Application.Interfaces.Services;
public interface ICustomerService : IGenericWriteRepository<CustomerRegisterRequest, CustomerUpdateRequest, Guid>, IGenericSpecificationReadRepository
{
    Task SaveEmailConfirmationToken(EmailConfirmationResult emailConfirmationResult, CancellationToken? cancellationToken);
    Task SavePassowordResetToken(ResetPasswordResult passwordResult, CancellationToken? cancellationToken);
    Task UpdatePassowordResetToken(PasswordChangeRequest passwordChangeRequest, CancellationToken? cancellationToken);
    Task UpdateEmailConfirmationToken(Guid id, CancellationToken? cancellationToken);
    Task RemoveProfilePicture(Guid id, CancellationToken? cancellationToken);
}