using BoardGameTracker.Core.Models.Dtos.Users;
using BoardGameTracker.Core.Models.Requests.Auth;

namespace BoardGameTracker.Core.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetCurrentUserAsync(Guid userId);
    Task<UserDto> UpdateProfileAsync(Guid userId, UpdateProfileRequest request);
    Task ChangePasswordAsync(Guid userId, ChangePasswordRequest request);
}
