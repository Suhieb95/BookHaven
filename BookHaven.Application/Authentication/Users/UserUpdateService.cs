using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Users;

namespace BookHaven.Application.Authentication.Users;
public class UserUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : IUserUpdateService
{
    public async Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = null)
    {
        User? currentUser = await _unitOfWork.Users.GetBy(new GetUserById(id), cancellationToken);
        if (currentUser is null)
            return Result<bool>.Failure(new Error("User Doesn't Exists.", NotFound, "User Not Found"));

        if (currentUser.ImageUrl is not null)
        {
            await _fileService.Delete(currentUser.ImageUrl);
            await _unitOfWork.UserSecurity.RemoveProfilePicture(id, cancellationToken, UserType.Customer);
        }

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Update(InternalUserUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        User? currentUser = await _unitOfWork.Users.GetBy(new GetUserByEmailAddress(request.EmailAddress), cancellationToken);
        if (currentUser is null)
            return Result<bool>.Failure(new Error("User Doesn't Exists.", NotFound, "User Not Found"));

        bool isDuplicateUserName = await _unitOfWork.Users.GetBy(new IsInternalUserUserNameUnique(request.UserName, currentUser.Id), cancellationToken);
        if (isDuplicateUserName)
            return Result<bool>.Failure(new Error("User Name already exists.", BadRequest, "Invalid User Name"));

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
        await _unitOfWork.Users.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
}