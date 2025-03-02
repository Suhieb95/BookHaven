using BookHaven.Application.Authentication.Common;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Users;
using BookHaven.Domain.Enums;
using BookHaven.Domain.Specification.Users;

namespace BookHaven.Application.Authentication.Users;
public class UserUpdateService(IUnitOfWork unitOfWork, IFileService fileService, IPasswordHasher passwordHasher) : IUserUpdateService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileService _fileService = fileService;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

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

        (bool flowControl, Result<bool>? result) = await CheckUserNameExistence.ValidateUserName<bool>(_unitOfWork, request.UserName, currentUser.Id, UserType.Internal, cancellationToken);
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
        await _unitOfWork.Users.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
}