using BookHaven.Domain.DTOs.BaseModels;

namespace BookHaven.Domain.DTOs.Users;
public class InternalUserRegisterRequest(string emailAddress, string userName) : RegisterBaseRequest(emailAddress, userName);