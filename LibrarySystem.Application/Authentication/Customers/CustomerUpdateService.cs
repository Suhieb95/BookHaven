using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Specification.Customers;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : ICustomerUpdateService
{
    public async Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        List<Customer>? currentUser = await _unitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(request.EmailAddress), cancellationToken);
        if (currentUser.FirstOrDefault() is null)
            return Result<bool>.Failure(new("Customer Doesn't Exists.", NotFound, "Customer Not Found"));

        if (request.IsValidToDeleteImage())
            await _fileService.Delete(request.ImageUrl!);
        else if (request.Image is not null)
        {
            FileUploadResult uploadResult = await _fileService.Upload(request.Image);
            request.ImageUrl = uploadResult.PublicId;
        }

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Customers.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
}