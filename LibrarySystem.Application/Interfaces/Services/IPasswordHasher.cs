namespace LibrarySystem.Application.Interfaces.Services;
public interface IPasswordHasher
{
    string Hash(string password);
    bool VerifyPassword(string passowrd, string hash);
}