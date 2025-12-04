using BoardGameTracker.Core.Models.Dtos.Users;

namespace BoardGameTracker.Core.Models.Responses.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}
