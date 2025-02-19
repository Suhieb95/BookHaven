using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.DTOs.Users;
public class InternalUserRegisterRequest(string emailAddress, string password, string userName) : RegisterRequestBase(emailAddress, password, userName);