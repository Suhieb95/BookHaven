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
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    private readonly RefreshJwtSettings _refreshJwtSettings;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings, IOptions<RefreshJwtSettings> refreshJwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
        _refreshJwtSettings = refreshJwtSettings.Value;
    }
    public async Task<string> GenerateAccessToken(PersonBase person, PersonType personType)
    {
        if (person is null)
            throw new JwtTokenExpception();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>{

            new Claim(ClaimTypes.Email,person.EmailAddress),
            new Claim(ClaimTypes.NameIdentifier,person.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub,_jwtSettings.Audience),
            new Claim(JwtRegisteredClaimNames.Iss,_jwtSettings.Issuer),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,JwtSettings.GetIssuedAt(), ClaimValueTypes.Integer64),
        };


        if (personType == PersonType.InternalUser)
        {
            // foreach (string role in userBaseModel.Roles)
            //     claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            // string[] userPermissions = await _IUserSerivce.GetUserPermissions(person.EmailAddress);
            // foreach (string permission in userPermissions)
            //     claims.Add(new Claim(PermissionsClaim.Permissions, permission));
        }

        var securityToken = new JwtSecurityToken(claims: [], issuer: _jwtSettings.Issuer, signingCredentials: signingCredentials, audience: _jwtSettings.Audience, expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public string GenerateRefreshToken(PersonBase person)
    {
        if (person is null)
            throw new JwtTokenExpception();

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshJwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss,_jwtSettings.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud,_jwtSettings.Audience),
            new Claim(ClaimTypes.NameIdentifier,person.Id.ToString()),
        };

        var securityToken = new JwtSecurityToken(claims: claims, issuer: _refreshJwtSettings.Issuer, signingCredentials: signingCredentials, audience: _refreshJwtSettings.Audience, expires: _dateTimeProvider.UtcNow.AddDays(_refreshJwtSettings.ExpiryDays));

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    public ResetPasswordResult GeneratePasswordResetToken(string emailAddress)
       => new(emailAddress, Convert.ToHexString(RandomNumberGenerator.GetBytes(120)), DateTime.Now.AddMinutes(30));
    public EmailConfirmationResult GenerateEmailConfirmationToken(Guid userId)
         => new(userId, Convert.ToHexString(RandomNumberGenerator.GetBytes(120)), DateTime.Now.AddMinutes(30));
}
