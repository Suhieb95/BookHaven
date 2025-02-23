using LibrarySystem.Application.Helpers;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Infrastructure.Services.Users;
public class UserService(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider) : IUserService
{
    public async Task LastLogin(string emailAddress, PersonType personType = PersonType.InternalUser)
    {
        string sql = $"UPDATE {personType.GetEnumValue()} SET LastLogin = @LastLogin WHERE EmailAddress = @EmailAddress";
        await _sqlDataAccess.SaveData(sql, new { EmailAddress = emailAddress, LastLogin = _dateTimeProvider.UtcNow });

    }
}