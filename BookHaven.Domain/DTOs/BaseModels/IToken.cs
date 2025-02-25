namespace BookHaven.Domain.DTOs.BaseModels;

public interface IToken
{
    string Token { get; }
    string RefreshToken { get; }
}
