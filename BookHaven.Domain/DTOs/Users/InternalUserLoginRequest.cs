using BookHaven.Domain.DTOs.BaseModels;

namespace BookHaven.Domain.DTOs.Users;
public class InternalUserLoginRequest(string emailAddress, string password) : LoginBaseRequest(emailAddress, password);

