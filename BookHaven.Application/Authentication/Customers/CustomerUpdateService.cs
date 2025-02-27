using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Customers;
using BookHaven.Domain.Specification.Customers;

namespace BookHaven.Application.Authentication.Customers;
public class CustomerUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : ICustomerUpdateService
{
    public async Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = null)
    {
        Customer? currentUser = (await _unitOfWork.Customers.GetAll(new GetCustomerById(id), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new Error("Customer Doesn't Exists.", NotFound, "Customer Not Found"));

        if (currentUser.ImageUrl is not null)
        {
            await _fileService.Delete(currentUser.ImageUrl);
            await _unitOfWork.UserSecurity.RemoveProfilePicture(id, cancellationToken);
        }

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Update(CustomerUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        Customer? currentUser = (await _unitOfWork.Customers.GetAll(new GetCustomerByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new Error("Customer Doesn't Exists.", NotFound, "Customer Not Found"));

        (bool flowControl, Result<bool>? result) = await CheckUserNameExistence.ValidateUserName<bool>(_unitOfWork, request.UserName, currentUser.Id, cancellationToken: cancellationToken);
        if (flowControl == false) return result!;

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