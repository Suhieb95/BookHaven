namespace LibrarySystem.Domain.DTOs.Users;
public record class RegisterResponse(ResponseBase UserInfo, string Token,
                                  string ImagePath, List<string> Permissions);
