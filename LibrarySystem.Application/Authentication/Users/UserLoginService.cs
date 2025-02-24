using LibrarySystem.Domain.DTOs.Users;

namespace LibrarySystem.Application.Authentication.Users;
public class UserLoginService(IUnitOfWork _unitOfWork) : IUserLoginService
{
    public async Task<Result<InternalUserLoginResponse>> Login(InternalUserLoginRequest request, CancellationToken? cancellationToken = null)
    {
        throw new NotImplementedException();
    }
}
