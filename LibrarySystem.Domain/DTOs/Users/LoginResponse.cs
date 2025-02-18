namespace LibrarySystem.Domain.DTOs.Users;
public record class LoginResponse(ResponseBase UserInfo, string Token,
                                  string ImagePath, List<string> Permissions);
