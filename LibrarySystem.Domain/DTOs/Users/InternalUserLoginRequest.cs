using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserLoginRequest(string emailAddress, string password) : LoginRequestBase(emailAddress, password);

