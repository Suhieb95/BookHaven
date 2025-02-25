using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Customers;
using LibrarySystem.Domain.Specification.Customers;

namespace LibrarySystem.Application.Authentication.Customers;
public class CustomerUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : ICustomerUpdateService
{
    public async Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? currentUser = (await _unitOfWork.Customers.GetAll(new GetCustomerById(id), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new("Customer Doesn't Exists.", NotFound, "Customer Not Found"));

        if (currentUser.ImageUrl is not null)
        {
            await _fileService.Delete(currentUser.ImageUrl);
            await _unitOfWork.Customers.RemoveProfilePicture(id, cancellationToken);
        }

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        Customer? currentUser = (await _unitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new("Customer Doesn't Exists.", NotFound, "Customer Not Found"));

        if (request.Image is not null)
        {
            FileUploadResult uploadResult = await _fileService.Upload(request.Image, cancellationToken);
            request.ImageUrl = uploadResult.PublicId;
            if (currentUser.ImageUrl is not null)
                await _fileService.Delete(currentUser.ImageUrl);
        }
        else
            request.ImageUrl = currentUser.ImageUrl; // Keep The same image

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Customers.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
}