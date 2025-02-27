using BookHaven.Domain.DTOs.Customers;
namespace BookHaven.Application.Interfaces.Services;
public interface ICustomerService : IGenericWriteRepository<CustomerRegisterRequest, CustomerUpdateRequest, Guid>, IGenericSpecificationReadRepository;