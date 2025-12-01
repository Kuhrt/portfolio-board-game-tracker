using BoardGameTracker.Core.Models.Requests.Auth;
using BoardGameTracker.Core.Models.Responses.Auth;

namespace BoardGameTracker.Core.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RegisterAsync(RegisterRequest request);
    Task<string> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task ResetPasswordAsync(ResetPasswordRequest request);
}
