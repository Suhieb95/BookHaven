using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Services;
public interface ICustomerService : IGenericReadWithParamRepository<List<Customer>, Specification>, IGenericWriteRepository<CustomerRegisterRequest, CustomerUpdateRequest, Guid>;