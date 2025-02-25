using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Specification.Users;

namespace LibrarySystem.Application.Authentication.Users;
public class UserUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : IUserUpdateService
{
    public async Task<Result<bool>> RemoveProfilePicture(Guid id, CancellationToken? cancellationToken = null)
    {
        User? currentUser = (await _unitOfWork.Users.GetAll(new GetUserById(id), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new("User Doesn't Exists.", NotFound, "User Not Found"));

        if (currentUser.ImageUrl is not null)
        {
            await _fileService.Delete(currentUser.ImageUrl);
            await _unitOfWork.Users.RemoveProfilePicture(id, cancellationToken);
        }

        return Result<bool>.Success(true);
    }
    public async Task<Result<bool>> Update(InternalUserUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        User? currentUser = (await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken)).FirstOrDefault();
        if (currentUser is null)
            return Result<bool>.Failure(new("User Doesn't Exists.", NotFound, "User Not Found"));

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