using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Specification.Customers;
using LibrarySystem.Domain.Specification.Users;
using Microsoft.Extensions.Options;
using static LibrarySystem.Application.Helpers.Extensions;

namespace LibrarySystem.Infrastructure.Authentication;
public class RefreshTokenValidator(IUnitOfWork _unitOfWork, IOptions<RefreshJwtSettings> refreshJwtSettings, IJwtTokenGenerator _IJwtTokenGenerator, IFileService _fileService) : IRefreshTokenValidator
{
    private readonly RefreshJwtSettings _refreshJwtSettings = refreshJwtSettings.Value;
    public async Task<Result<RefreshToken>> ValidateRefreshToken(string? token, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Result<RefreshToken>.Failure(new("Cancelled", HttpStatusCode.NoContent, "Operation Cancelled"));

        if (string.IsNullOrEmpty(token))
            return Result<RefreshToken>.Failure(new("Either Refresh Token is invalid or expired.", HttpStatusCode.Forbidden, "Invalid Token"));

        var (userId, userType) = await ExtractUserClaims(token);

        if (string.IsNullOrEmpty(userId))
            return Result<RefreshToken>.Failure(new("Either Refresh Token is invalid or expired.", HttpStatusCode.Forbidden, "Invalid Token"));

        UserType type = Enum.TryParse<UserType>(userType, true, out var parsedType) ? parsedType : UserType.Customer;
        Guid guidId = Guid.Parse(userId);

        string? imageUrl = string.Empty;
        PersonBase? user = await GetPersonType(type, guidId);
        string accessToken = await GenerateAccessToken(type, user!);
        if (!string.IsNullOrEmpty(user!.ImageUrl))
            imageUrl = await _fileService.GetFile(user!.ImageUrl);

        RefreshToken refreshToken = new(
            user.Id,
            user.EmailAddress,
            user.UserName,
            accessToken,
            imageUrl ?? string.Empty);

        return Result<RefreshToken>.Success(refreshToken);
    }
    private async Task<PersonBase?> GetPersonType(UserType userType, Guid id)
        => userType switch
        {
            UserType.Customer => (await _unitOfWork.Customers.GetAll(new GetCustomerById(id))).FirstOrDefault()!,
            UserType.Internal => (await _unitOfWork.Users.GetAll(new GetUserById(id))).FirstOrDefault()!,
            _ => null
        };
    private async Task<string> GenerateAccessToken(UserType userType, PersonBase person)
    {
        if (userType == UserType.Internal)
        {
            (string[] roles, string[] permissions) = await AddUserPermissionsAndRoles(person.Id);
            return _IJwtTokenGenerator.GenerateAccessToken(person, UserType.Internal, roles, permissions);
        }
        return _IJwtTokenGenerator.GenerateAccessToken(person);
    }
    private async Task<(string[] roles, string[] permissions)> AddUserPermissionsAndRoles(Guid id, CancellationToken? cancellationToken = default)
    {
        string[] roles = await _unitOfWork.Users.GetUserRoles(id, cancellationToken);
        string[] permissions = await _unitOfWork.Users.GetUserPermissions(id, cancellationToken);
        return (roles, permissions);
    }
    private async Task<(string, string)> ExtractUserClaims(string token)
        => await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = ValidateJwtToken(_refreshJwtSettings);
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            string? userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string? userType = principal.FindFirst("UserType")?.Value;
            return (userId, userType);
        });
}