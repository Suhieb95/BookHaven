using BookHaven.Application.Interfaces.Services;
using static BCrypt.Net.BCrypt;

namespace BookHaven.Infrastructure.Services;
public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => HashPassword(password);

    public bool VerifyPassword(string password, string hash)
        => Verify(password, hash);
}
