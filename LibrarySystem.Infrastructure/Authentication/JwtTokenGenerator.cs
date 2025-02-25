using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.BaseModels.User;
using LibrarySystem.Domain.DTOs.Auth;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using LibrarySystem.Domain.Exceptions.UserExceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibrarySystem.Infrastructure.Authentication;
public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings, IOptions<RefreshJwtSettings> refreshJwtSettings)
     : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly RefreshJwtSettings _refreshJwtSettings = refreshJwtSettings.Value;
    public string GenerateAccessToken(PersonBase person, UserType personType = UserType.Customer, string[]? roles = null, string[]? permissions = null)
    {
        if (person is null)
            throw new JwtTokenExpception();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>{

            new(ClaimTypes.Email,person.EmailAddress),
            new(ClaimTypes.NameIdentifier,person.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat,JwtSettings.GetIssuedAt(), ClaimValueTypes.Integer64),
            new("PersonType",personType.ToString(), ClaimValueTypes.Integer64),
        };

        if (personType == UserType.Internal)
            AddUserPermissionsAndRoles(claims, roles!, permissions!);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtSettings.Issuer,
            signingCredentials: signingCredentials,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public string GenerateRefreshToken(PersonBase person, UserType personType = UserType.Customer)
    {
        if (person is null)
            throw new JwtTokenExpception();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshJwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,JwtSettings.GetIssuedAt(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.NameIdentifier,person.Id.ToString()),
            new("UserType",personType.ToString(), ClaimValueTypes.Integer64),
        };

        var securityToken = new JwtSecurityToken(
            claims: claims,
            issuer: _refreshJwtSettings.Issuer,
            signingCredentials: signingCredentials,
            audience: _refreshJwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddDays(_refreshJwtSettings.ExpiryDays));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public ResetPasswordResult GeneratePasswordResetToken(string emailAddress)
       => new(emailAddress, Convert.ToHexString(RandomNumberGenerator.GetBytes(120)), DateTime.Now.AddMinutes(30));
    public EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId)
         => new(userId, Convert.ToHexString(RandomNumberGenerator.GetBytes(120)), DateTime.Now.AddDays(1));

    private static void AddUserPermissionsAndRoles(List<Claim> claims, string[] roles, string[] permissions)
    {
        foreach (string role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

        foreach (string permission in permissions)
            claims.Add(new Claim(PermissionsClaim.Permissions, permission));
    }
}