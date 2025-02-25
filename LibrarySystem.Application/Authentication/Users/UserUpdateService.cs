using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Users;
using LibrarySystem.Domain.Specification.Users;

namespace LibrarySystem.Application.Authentication.Users;
public class UserUpdateService(IUnitOfWork _unitOfWork, IFileService _fileService, IPasswordHasher _passwordHasher) : IUserUpdateService
{
    public async Task<Result<bool>> Update(InternalUserUpdateRequest request, CancellationToken? cancellationToken = default)
    {
        List<User>? currentUser = await _unitOfWork.Users.GetAll(new GetUserByEmailAddress(request.EmailAddress), cancellationToken);
        if (currentUser.FirstOrDefault() is null)
            return Result<bool>.Failure(new("User Doesn't Exists.", NotFound, "User Not Found"));

        if (request.IsValidToDeleteImage())
            await _fileService.Delete(request.ImageUrl!);
        else if (request.Image is not null)
        {
            FileUploadResult uploadResult = await _fileService.Upload(request.Image);
            request.ImageUrl = uploadResult.PublicId;
        }

        request.SetPassword(_passwordHasher.Hash(request.Password));
        await _unitOfWork.Users.Update(request, cancellationToken);
        return Result<bool>.Success(true);
    }
}